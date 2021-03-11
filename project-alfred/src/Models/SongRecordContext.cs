using Microsoft.EntityFrameworkCore;

namespace project_alfred.models
{
    public class SongRecordContext : DbContext
    {
        public DbSet<SongRecord> SongRecords { get; set; }
        public DbSet<FetchLog> FetchLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FetchLog>()
                .Property(f => f.CreatedAt)
                .HasDefaultValueSql("now()");

            modelBuilder.Entity<SongRecord>()
                .HasKey(s => new {s.user, s.url});
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Host=192.168.192.1;Database=db_for_everything;Username=root;Password=development");
        }
    }
}