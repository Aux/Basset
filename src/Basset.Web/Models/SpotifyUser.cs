using Newtonsoft.Json;

namespace Basset.Web
{
    public class SpotifyUser
    {
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("href")]
        public string Href { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("product")]
        public string ProductType { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}
