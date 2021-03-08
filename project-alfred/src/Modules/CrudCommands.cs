using System;
using System.Threading.Tasks;

using Discord.Commands;

using project_alfred.models;
using project_alfred.TypeReaders;

namespace project_alfred.Modules
{
    public class CrudCommands : ModuleBase<SocketCommandContext>
    {
        [Command("add")]
        public async Task AddSongRecord(Url url)
        {
            var std = new SongRecord()
            {
                user = Context.User.Id,
                url = url.Value
            };
            
            var context = new SongRecordContext();
            context.Add(std);
            await context.SaveChangesAsync();

            await ReplyAsync($"🚀 Song was added!");
        }
    }
}