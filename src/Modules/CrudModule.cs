using System.Threading.Tasks;

using Discord.Commands;

namespace project_alfred.Modules
{
    public class CrudModule : ModuleBase<SocketCommandContext>
    {
        [Command("add")]
        [Summary("TODO: add")]
        public async Task Add()
        {
            await ReplyAsync("Hello, World!");
        }
    }
}