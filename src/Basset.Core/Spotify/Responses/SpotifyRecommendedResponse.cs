using Newtonsoft.Json;
using System.Collections.Generic;

namespace Basset.Spotify
{
    public class SpotifyRecommendedResponse
    {
        [JsonProperty("tracks")]
        public List<SpotifySimpleTrack> Tracks { get; set; }
        // Not implemented
        public List<object> Seeds { get; set; }
    }
}
