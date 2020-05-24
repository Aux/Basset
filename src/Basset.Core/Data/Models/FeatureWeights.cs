using System.ComponentModel.DataAnnotations;

namespace Basset.Data
{
    public class FeatureWeights
    {
        public ulong Id { get; set; }
        public ulong GuildId { get; set; }
        public bool? Mode { get; set; }
        [Range(-1, 11)]
        public int? Key { get; set; }
        [Range(1, int.MaxValue)]
        public int? TimeSignature { get; set; }
        [Range(0.1, 1.0)]
        public float? Danceability { get; set; }
        [Range(0.1, 1.0)]
        public float? Energy { get; set; }
        [Range(-60, 0)]
        public float? Loudness { get; set; }
        [Range(0.1, 1.0)]
        public float? Speechiness { get; set; }
        [Range(0.1, 1.0)]
        public float? Acousticness { get; set; }
        [Range(0.1, 1.0)]
        public float? Instrumentalness { get; set; }
        [Range(0.1, 1.0)]
        public float? Liveness { get; set; }
        [Range(0.1, 1.0)]
        public float? Valence { get; set; }
        [Range(0, 250)]
        public float? Tempo { get; set; }

        public Guild Guild { get; set; }
    }
}
