using Discord.Commands;
using Microsoft.Extensions.Logging;

namespace project_alfred.Commands;

[Group("song")]
public class SongModule : ModuleBase<SocketCommandContext>
{
    private readonly ILogger<SongModule> _logger;

    public SongModule(ILogger<SongModule> logger)
    {
        _logger = logger;
    }
    
    [Command("add")]
    public Task AddSong()
    {
        _logger.LogInformation("Adding song");
        return Task.CompletedTask;
    }
}