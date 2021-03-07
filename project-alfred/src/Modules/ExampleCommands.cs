using System.Threading.Tasks;

using Discord.Commands;

namespace project_alfred.Modules
{
    public class ExampleCommands : ModuleBase
    {
        [Command("test")]
        public async Task Hello()
        {
            await ReplyAsync("Hello bro!");
        }
    }
}