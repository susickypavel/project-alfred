using Discord;
using Discord.Commands;
using Domain.Context;
using Domain.Entity;
using Microsoft.Extensions.Logging;
using project_alfred.TypeReaders;

namespace project_alfred.Commands;

[Group("song")]
public class SongModule : ModuleBase<SocketCommandContext>
{
    private readonly ILogger<SongModule> _logger;
    private readonly ProjectAlfredContext _context;

    public SongModule(ILogger<SongModule> logger, ProjectAlfredContext context)
    {
        _logger = logger;
        _context = context;
    }
    
    [Command("add")]
    [Summary("Adds song to database.")]
    public async Task AddSong([Summary("URL of the song to be added.")] Url url)
    {
        try
        {
            var song = await _context.Songs.FindAsync(url.Value, Context.User.ToString());

            if (song != null)
            {
                _logger.LogInformation("User '{User}' tried to add '{Url}', which he already had", Context.User.ToString(), url.Value);
                await ReplyAsync("Song already added");
                return;
            }

            await _context.Songs.AddAsync(new SongRecord()
            {
                OriginalPoster = Context.User.ToString(),
                OriginalUrl = url.Value
            });
            await _context.SaveChangesAsync();
            
            var message = await ReplyAsync("Song added");
            
            await message.AddReactionAsync(Emoji.Parse("👍"));
            await message.AddReactionAsync(Emoji.Parse("🦄"));

            _logger.LogInformation("User '{User}' added '{Url}'", Context.User.ToString(), url.Value);
        }
        catch (Exception e)
        {
            _logger.LogError("{Message}", e.Message);
            await ReplyAsync("Something went wrong");
        }
    }
}