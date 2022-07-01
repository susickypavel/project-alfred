using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Domain.Context;

public class ProjectAlfredContext : DbContext
{
    public DbSet<SongRecord> Songs { get; set; }

    private string DbPath { get; }

    public ProjectAlfredContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "project_alfred.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SongRecord>().HasKey(sr => new { sr.OriginalUrl, sr.OriginalPoster });
    }
}
