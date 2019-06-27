using Basset.Logging;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Basset.Services
{
    public class StartupService : IBackgroundService
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _config;
        private readonly DiscordShardedClient _discord;
        private readonly CommandService _commands;

        public StartupService(
            ILoggerFactory loggerFactory,
            IServiceProvider serviceProvider,
            IConfiguration config,
            DiscordShardedClient discord,
            CommandService commands)
        {
            _loggerFactory = loggerFactory;
            _serviceProvider = serviceProvider;
            _config = config;
            _discord = discord;
            _commands = commands;
        }

        public void Start()
            => StartAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        public void Stop()
            => throw new NotImplementedException();

        public async Task StartAsync()
        {
            var fileLoggerOptions = new LoggingConfig();
            _config.Bind("logging", fileLoggerOptions);
            _loggerFactory.AddProvider(new BotLoggerProvider(fileLoggerOptions));

            await _discord.LoginAsync(Discord.TokenType.Bot, _config["discord_token"]);
            await _discord.StartAsync();

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _serviceProvider);
        }
    }
}
