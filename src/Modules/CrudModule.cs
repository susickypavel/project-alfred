using System.Threading.Tasks;

using Discord.Commands;
using project_alfred.TypeReaders;

namespace project_alfred.Modules
{
    public class CrudModule : ModuleBase<SocketCommandContext>
    {
        [Command("add")]
        [Summary("TODO: add")]
        public async Task Add(Url url)
        {
            await ReplyAsync($"Hello, World! {url.Value} is {url.IsValid}");
        }
    }
}