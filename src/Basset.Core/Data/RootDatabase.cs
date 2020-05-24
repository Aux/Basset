using Basset.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Text;

namespace Basset.Data
{
    public class RootDatabase : DbContext
    {
        private readonly DataOptions _options;

        public DbSet<SpotifyTrack> Tracks { get; set; }
        public DbSet<SpotifyListen> Listens { get; set; }

        public DbSet<Guild> Guilds { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<GuildWeight> GuildWeights { get; set; }
        public DbSet<FeatureWeights> FeatureWeights { get; set; }

        public RootDatabase(IConfiguration config)
        {
            _options = new DataOptions();
            config.Bind("data", _options);

            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_options.ServerType == ServerType.SQLite)
            {
                string baseDir = Path.Combine(AppContext.BaseDirectory, "common/data");
                if (!Directory.Exists(baseDir))
                    Directory.CreateDirectory(baseDir);

                string datadir = Path.Combine(baseDir, "basset.sqlite.db");
                optionsBuilder.UseSqlite($"Filename={datadir}");
            } else
            {
                var stringBuilder = new StringBuilder();
                if (string.IsNullOrWhiteSpace(_options.Host))
                    throw new ArgumentNullException("`host` value in configuration is missing");
                else
                    stringBuilder.Append($"Host={_options.Host};");
                if (string.IsNullOrWhiteSpace(_options.Database))
                    throw new ArgumentNullException("`database` value in configuration is missing");
                else
                    stringBuilder.Append($"Database={_options.Database};");
                if (!string.IsNullOrWhiteSpace(_options.User))
                    stringBuilder.Append($"Username={_options.User};");
                if (!string.IsNullOrWhiteSpace(_options.Password))
                    stringBuilder.Append($"Password={_options.Password};");

                if (_options.ServerType == ServerType.MySQL)
                    optionsBuilder.UseMySql(stringBuilder.ToString());
                else if (_options.ServerType == ServerType.Postgres)
                    optionsBuilder.UseNpgsql(stringBuilder.ToString());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SpotifyTrack>()
                .HasMany(x => x.Listens)
                .WithOne(x => x.Track);
            modelBuilder.Entity<SpotifyListen>()
                .HasOne(x => x.Track)
                .WithMany(x => x.Listens);

            modelBuilder.Entity<Guild>()
                .HasMany(x => x.GuildWeights)
                .WithOne(x => x.Guild);
            modelBuilder.Entity<Guild>()
                .HasOne(x => x.FeatureWeights)
                .WithOne(x => x.Guild);
        }
    }
}
