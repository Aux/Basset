using Newtonsoft.Json;
using System.Collections.Generic;

namespace Basset.Spotify
{
    public class SpotifySearchResponse
    {
        [JsonProperty("tracks")]
        public SpotifySearchCollection<SpotifySimpleTrack> Tracks { get; set; }
        [JsonProperty("artists")]
        public SpotifySearchCollection<SpotifySimpleArtist> Artists { get; set; }
        //[JsonProperty("albums")]
        //public List<object> Albums { get; set; }
        //[JsonProperty("playlists")]
        //public List<object> Playlists { get; set; }
    }

    public class SpotifySearchCollection<T>
    {
        [JsonProperty("href")]
        public string Href { get; set; }
        [JsonProperty("items")]
        public List<T> Items { get; set; }
    }
}
