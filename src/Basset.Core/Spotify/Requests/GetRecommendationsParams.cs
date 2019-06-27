using Newtonsoft.Json;
using System.Collections.Generic;

namespace Basset.Spotify
{
    // https://developer.spotify.com/documentation/web-api/reference/browse/get-recommendations/
    public class GetRecommendationsParams : QueryMap
    {
        [JsonProperty("limit")]
        public int Limit { get; set; }
        [JsonProperty("market")]
        public string Market { get; set; }

        // 5 entries total between these 3 properties
        [JsonProperty("seed_artists")]
        public List<string> SeedArtists { get; set; }
        [JsonProperty("seed_genres")]
        public List<string> SeedGenres { get; set; }
        [JsonProperty("seed_tracks")]
        public List<string> SeedTracks { get; set; }

        // Not implemented because of TuneableTrackAttributes
        [JsonProperty("max_*")]
        public List<string> Max { get; set; }
        [JsonProperty("min_*")]
        public List<string> Min { get; set; }
        [JsonProperty("target_*")]
        public List<string> Target { get; set; }

        public override IDictionary<string, string> CreateQueryMap()
        {
            var dict = new Dictionary<string, string>();
            dict["limit"] = Limit.ToString();
            if (SeedArtists != null) dict["seed_artists"] = string.Join(",", SeedArtists);
            if (SeedGenres != null) dict["seed_genres"] = string.Join(",", SeedGenres);
            if (SeedTracks != null) dict["seed_tracks"] = string.Join(",", SeedTracks);
            return dict;
        }
    }

    public enum TuneableTrackAttributes
    {
        Accousticness,
        Danceability,
        DurationMs,
        Energy,
        Instrumentalness,
        Key,
        Liveness,
        Loudness,
        Mode,
        Popularity,
        Speechiness,
        Tempo,
        TimeSignature,
        Valence
    }
}
