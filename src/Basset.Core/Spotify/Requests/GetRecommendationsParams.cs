using Newtonsoft.Json;
using System.Collections.Generic;

namespace Basset.Spotify
{
    // https://developer.spotify.com/documentation/web-api/reference/browse/get-recommendations/
    public class GetRecommendationsParams : QueryMap
    {
        public int Limit { get; set; }
        public string Market { get; set; }

        // 5 entries total between these 3 properties
        public List<string> SeedArtists { get; set; }
        public List<string> SeedGenres { get; set; }
        public List<string> SeedTracks { get; set; }

        // Not implemented because of TuneableTrackAttributes
        public List<string> Max { get; set; }
        public List<string> Min { get; set; }
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
