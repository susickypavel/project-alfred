using System.Threading.Tasks;

using Discord.Commands;

namespace project_alfred.Modules
{
    public class CrudCommands : ModuleBase
    {
        [Command("add")]
        public async Task AddSongRecord(string url)
        {
            await ReplyAsync($"TODO: implement");
        }
    }
}