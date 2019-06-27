using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basset.Services
{
    public class CommandHandlingService : IBackgroundService
    {
        private ConcurrentDictionary<ulong, DateTime> _users;
        private readonly ILogger<CommandHandlingService> _logger;
        private readonly IServiceProvider _provider;
        private readonly DiscordShardedClient _discord;
        private readonly CommandService _commands;

        public CommandHandlingService(
            ILogger<CommandHandlingService> logger,
            IServiceProvider provider,
            DiscordShardedClient discord,
            CommandService commands)
        {
            _users = new ConcurrentDictionary<ulong, DateTime>();
            _logger = logger;
            _provider = provider;
            _discord = discord;
            _commands = commands;
        }

        public void Start()
        {
            _discord.MessageReceived += OnMessageReceivedAsync;
            _logger.LogInformation("Started");
        }

        public void Stop()
        {
            _discord.MessageReceived -= OnMessageReceivedAsync;
            _logger.LogInformation("Stopped");
        }

        public bool IsRatelimited(ulong userId)
        {
            if (_users.TryRemove(userId, out DateTime expires))
            {
                if (expires < DateTime.Now) return true;
                _users.TryAdd(userId, DateTime.Now.AddMinutes(1));
            }

            return false;
        }

        private Task OnMessageReceivedAsync(SocketMessage s)
        {
            _ = Task.Run(async () =>
            {
                if (!(s is SocketUserMessage msg)) return;

                int argPos = 0;
                var context = new ShardedCommandContext(_discord, msg);
                if (msg.HasMentionPrefix(_discord.CurrentUser, ref argPos))
                {
                    if (IsRatelimited(context.User.Id)) return;

                    using (context.Channel.EnterTypingState())
                    {
                        await ExecuteAsync(context, _provider, context.Message.Content.Substring(argPos));
                    }
                }
            });
            return Task.CompletedTask;
        }

        public async Task ExecuteAsync(ShardedCommandContext context, IServiceProvider provider, string input)
        {
            var result = await _commands.ExecuteAsync(context, input, provider);
            if (result.IsSuccess) return;

            if (result is ExecuteResult execute)
                _logger.LogError(execute.Exception?.ToString());

            if (result is ParseResult parse && parse.Error == CommandError.BadArgCount)
                await SendHelpTextAsync(context, parse, input);
            else
            if (!string.IsNullOrWhiteSpace(result.ErrorReason))
                await context.Channel.SendMessageAsync(result.ErrorReason);
        }

        private async Task SendHelpTextAsync(ShardedCommandContext context, ParseResult result, string input)
        {
            var command = _commands.Search(context, input).Commands
                .OrderByDescending(x => x.Command.Parameters.Count())
                .FirstOrDefault().Command;

            var builder = new StringBuilder(context.Client.CurrentUser.Mention + " " + command.Name);
            if (command.Parameters.Count > 0)
            {
                // !name <required> [optional=1]
                foreach (var arg in command.Parameters)
                {
                    string argText = arg.Name;

                    if (arg.IsRemainder)
                        argText += "...";
                    if (arg.IsMultiple)
                        argText += "+";
                    if (arg.IsOptional)
                        argText = $"[{argText + (arg.DefaultValue != null ? "=" + arg.DefaultValue : "")}]";
                    else
                        argText = $"<{argText}>";

                    builder.Append(" " + argText);
                }
            }

            await context.Channel.SendMessageAsync($"{result.ErrorReason} {builder}");
        }
    }
}
