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

        public CollectingService(ILogger<CollectingService> logger, DiscordShardedClient discord, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _discord = discord;
        }

        public async Task RunAsync()
        {
            _discord.GuildMemberUpdated += OnGuildMemberUpdatedAsync;
            _discord.UserUpdated += OnUserUpdatedAsync;

            await _discord.LoginAsync(TokenType.Bot, _config["discord:token"]);
            await _discord.StartAsync();
            await Task.Delay(-1);
        }

        private Task OnUserUpdatedAsync(SocketUser before, SocketUser after)
        {
            var bspotify = before.Activity as SpotifyGame;
            var aspotify = after.Activity as SpotifyGame;

            if (bspotify == null && aspotify == null)
                return Task.CompletedTask;

            _logger.LogInformation($"User: {bspotify} -> {aspotify}");
            return Task.CompletedTask;
        }

        private Task OnGuildMemberUpdatedAsync(SocketGuildUser before, SocketGuildUser after)
        {
            var bspotify = before.Activity as SpotifyGame;
            var aspotify = after.Activity as SpotifyGame;

            if (bspotify == null && aspotify == null)
                return Task.CompletedTask;
            if (bspotify?.TrackId == aspotify?.TrackId)
                return Task.CompletedTask;

            _logger.LogInformation($"Guild: {bspotify} -> {aspotify}");
            return Task.CompletedTask;
        }
    }
}
