using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entity;

public class SongContext : DbContext
{
    public DbSet<SongRecord> Songs { get; set; }

    private string DbPath { get; }

    public SongContext()
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

public class SongRecord
{
    public string OriginalUrl { get; set; }
    
    public string OriginalPoster { get; set; }
}