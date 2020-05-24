namespace Basset.Data
{
    public enum WeightType
    {
        User,
        Role
    }

    public class GuildWeight
    {
        public ulong Id { get; set; }
        public ulong GuildId { get; set; }
        public ulong WeightedId { get; set; }
        public WeightType WeightType { get; set; }

        public Guild Guild { get; set; }
    }
}
