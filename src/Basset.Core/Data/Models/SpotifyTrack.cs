namespace Basset.Data
{
    public class SpotifyTrack
    {
        public string Id { get; set; }
        public string Artists { get; set; }
        public string AlbumArtUrl { get; set; }
        public string AlbumTitle { get; set; }
        public string TrackTitle { get; set; }
        public long? Duration { get; set; }
    }
}
