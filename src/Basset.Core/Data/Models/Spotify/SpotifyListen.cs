using System;

namespace Basset.Data
{
    public class SpotifyListen
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public ulong UserId { get; set; }
        public ulong GuildId { get; set; }
        public string TrackId { get; set; }

        public SpotifyTrack Track { get; set; }
    }
}
