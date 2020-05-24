using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Basset.Collector
{
    public class CollectingService
    {
        private readonly ILogger<CollectingService> _logger;
        private readonly IConfiguration _config;
        private readonly DiscordShardedClient _discord;

        public CollectingService(ILogger<CollectingService> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _discord = new DiscordShardedClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose
            });
        }

        public async Task RunAsync()
        {
            _logger.LogInformation("Starting");
            _discord.GuildMemberUpdated += OnGuildMemberUpdatedAsync;

            await _discord.LoginAsync(TokenType.Bot, _config["discord:token"]);
            await _discord.StartAsync();
            _logger.LogInformation("Running");
        }

        private Task OnGuildMemberUpdatedAsync(SocketGuildUser before, SocketGuildUser after)
        {
            throw new NotImplementedException();
        }
    }
}
