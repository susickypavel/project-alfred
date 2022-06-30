using Discord.Commands;

namespace project_alfred.Commands;

[Group("song")]
public class SongModule : ModuleBase<SocketCommandContext>
{
    [Command("last")]
    public async Task GetLastSong()
    {
        await ReplyAsync("Hello, World!");
    }
}