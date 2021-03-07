using System.Threading.Tasks;

using Discord.Commands;
using project_alfred.models;

namespace project_alfred.Modules
{
    public class CrudCommands : ModuleBase<SocketCommandContext>
    {
        [Command("add")]
        public async Task AddSongRecord(string url)
        {
            // var std = new SongRecord()
            // {
            //     user = Context.User.Id,
            //     url = url
            // };
            //
            // var context = new SongRecordContext();
            // context.Add(std);
            // await context.SaveChangesAsync();
            
            await ReplyAsync($"TODO: implement");
        }
    }
}