using Newtonsoft.Json;

namespace Basset.Spotify
{
    public class CreatePlaylistParams
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; } = null;
    }
}
