using Newtonsoft.Json;
using System.Collections.Generic;

namespace Basset.Spotify
{
    public class SpotifySimpleTrack
    {
        [JsonProperty("artists")]
        public List<SpotifySimpleArtist> Artists { get; set; }
        [JsonProperty("disc_number")]
        public int DiscNumber { get; set; }
        [JsonProperty("duration_ms")]
        public long Duration { get; set; }
        [JsonProperty("explicit")]
        public bool IsExplicit { get; set; }
        [JsonProperty("external_urls")]
        public Dictionary<string, string> ExternalUrls { get; set; }
        [JsonProperty("href")]
        public string Href { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("is_playable")]
        public bool IsPlayable { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("preview_url")]
        public string PreviewUrl { get; set; }
        [JsonProperty("track_number")]
        public int TrackNumber { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}
