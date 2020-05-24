using System;
using System.Collections.Generic;

namespace Basset.Data
{
    public class SpotifyTrack
    {
        // Provided by Discord
        public string Id { get; set; }
        public string Title { get; set; }
        public double Duration { get; set; }

        // Provided by Spotify
        public DateTime? ReleaseDate { get; set; } = null;
        public int? Popularity { get; set; } = null;

        public List<SpotifyListen> Listens { get; set; }
    }
}
