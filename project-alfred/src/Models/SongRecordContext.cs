using Microsoft.EntityFrameworkCore;

namespace project_alfred.models
{
    public class SongRecordContext : DbContext
    {
        public DbSet<SongRecord> SongRecords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Host=192.168.192.1;Database=DiscordBotAlfred;Username=postgres;Password=development");
        }
    }
}