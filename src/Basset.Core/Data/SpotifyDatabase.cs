using Microsoft.EntityFrameworkCore;

namespace Basset.Data
{
    public class SpotifyDatabase : DbContext
    {
        public DbSet<SpotifyTrack> Tracks { get; set; }
        public DbSet<SpotifyListen> Listens { get; set; }

        public SpotifyDatabase()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename=spotify.sqlite.db");
        }
    }
}
