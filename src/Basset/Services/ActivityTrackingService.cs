using Basset.Data;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Basset.Services
{
    public class ActivityTrackingService : IBackgroundService
    {
        private readonly ILogger<ActivityTrackingService> _logger;
        private readonly DiscordShardedClient _discord;
        private readonly SpotifyDatabase _db;

        public ActivityTrackingService(
            ILogger<ActivityTrackingService> logger,
            DiscordShardedClient discord,
            SpotifyDatabase db)
        {
            _logger = logger;
            _discord = discord;
            _db = db;
        }

        public void Start()
        {
            _discord.GuildMemberUpdated += OnGuildMemberUpdatedAsync;
        }

        public void Stop()
            => throw new NotImplementedException();

        private async Task OnGuildMemberUpdatedAsync(SocketGuildUser before, SocketGuildUser after)
        {
            if (before.Activity == after.Activity)
                return;
            if (!(after.Activity is SpotifyGame activity))
                return;

            if (!_db.Tracks.Any(x => x.Id == activity.TrackId))
            {
                var track = new SpotifyTrack
                {
                    Id = activity.TrackId,
                    Artists = string.Join(", ", activity.Artists),
                    AlbumArtUrl = activity.AlbumArtUrl,
                    AlbumTitle = activity.AlbumTitle,
                    TrackTitle = activity.TrackTitle
                };

                if (activity.Duration.HasValue)
                    track.Duration = activity.Duration.Value.Ticks;

                await _db.Tracks.AddAsync(track);
            }

            await _db.Listens.AddAsync(new SpotifyListen
            {
                GuildId = after.Guild.Id,
                UserId = after.Id,
                TrackId = activity.TrackId
            });
            await _db.SaveChangesAsync();

            _logger.LogInformation($"{after.Guild}: {activity.ToString()}");
        }
    }
}
