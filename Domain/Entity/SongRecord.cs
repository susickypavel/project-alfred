namespace Domain.Entity;
public class SongRecord
{
    public string OriginalUrl { get; set; }
    
    public string OriginalPoster { get; set; }

    public int LikesCount { get; set; } = 0;

    public int UnicornsCount { get; set; } = 0;
}