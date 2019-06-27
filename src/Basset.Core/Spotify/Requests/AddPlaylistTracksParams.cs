using Newtonsoft.Json;
using System.Collections.Generic;

namespace Basset.Spotify
{
    public class AddPlaylistTracksParams
    {
        [JsonProperty("uris")]
        public List<string> Uris { get; set; } = new List<string>();
        [JsonProperty("position")]
        public int Position { get; set; }

        public void AddTracks(params string[] trackIds)
        {
            foreach (var trackId in trackIds)
                Uris.Add(string.Format(SpotifyConstants.TrackUriFormat, trackId));
        }
    }
}
