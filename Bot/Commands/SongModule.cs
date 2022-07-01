using Discord.Commands;
using Domain.Entity;
using Microsoft.Extensions.Logging;

namespace project_alfred.Commands;

[Group("song")]
public class SongModule : ModuleBase<SocketCommandContext>
{
    private readonly ILogger<SongModule> _logger;
    
    private readonly SongContext db = new();

    public SongModule(ILogger<SongModule> logger)
    {
        _logger = logger;
    }
    
    [Command("add")]
    public async Task GetLastSong()
    {
        try
        {
            await db.Songs.AddAsync(new SongRecord()
            {
                OriginalPoster = Context.User.ToString(),
                OriginalUrl = "TODO"
            });

            await db.SaveChangesAsync();
            await ReplyAsync("Song added!");
        }
        catch (Exception e)
        {
            _logger.LogError("{Message}", e.InnerException.Message);
            await ReplyAsync("Song couldn't be added.");
        }
    }
}