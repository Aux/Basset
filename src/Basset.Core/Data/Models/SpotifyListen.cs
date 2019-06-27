using System;

namespace Basset.Data
{
    public class SpotifyListen
    {
        public DateTime Timestamp { get; set; }
        public ulong Id { get; set; }
        public ulong GuildId { get; set; }
        public ulong UserId { get; set; }
        public string TrackId { get; set; }
        public long? Duration { get; set; }
    }
}
