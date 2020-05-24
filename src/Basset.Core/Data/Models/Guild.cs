using System.Collections.Generic;

namespace Basset.Data
{
    public enum OptMode
    {
        OptOut,
        OptIn
    }

    public class Guild
    {
        public ulong Id { get; set; }
        public bool IsBlocked { get; set; }
        public OptMode OptMode { get; set; }

        public FeatureWeights FeatureWeights { get; set; }
        public List<GuildWeight> GuildWeights { get; set; }
    }
}
