using Microsoft.EntityFrameworkCore;

namespace Basset.Data
{
    public class RootDatabase : DbContext
    {
        public DbSet<object> Activities { get; set; }

        public RootDatabase()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename=root.sqlite.db");
        }
    }
}
