using Newtonsoft.Json;

namespace Basset.Spotify
{
    public class ModifyPlaylistParams
    {
        [JsonProperty("")]
        public string Name { get; set; }
        [JsonProperty("")]
        public string Description { get; set; }
        [JsonProperty("")]
        public bool IsPublic { get; set; }
    }
}
