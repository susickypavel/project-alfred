using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;

using project_alfred.models;
using project_alfred.TypeReaders;

namespace project_alfred.Modules
{
    public class CrudCommands : ModuleBase<SocketCommandContext>
    {
        [Command("add")]
        public async Task AddSongRecord(Url url)
        {
            using (var context = new SongRecordContext())
            {
                try
                {
                    context.Add(new SongRecord()
                    {
                        user = Context.User.Id,
                        url = url.Value,
                        publishedAt = Context.Message.Timestamp.UtcDateTime
                    });
                    
                    await context.SaveChangesAsync();
                    await ReplyAsync($"🚀 Song was added!");
                }
                catch (DbUpdateException error)
                {
                    await ReplyAsync("Tenhle song uz jsi pridal :)");
                }
            }
        }

        [Command("remove")]
        public async Task RemoveSongRecord(Url url)
        {
            using (var db = new SongRecordContext())
            {
                try
                {
                    var song = await db.SongRecords.FindAsync(Context.User.Id, url.Value);

                    if (song == null)
                    {
                        await ReplyAsync("Sorry, ale vypadá to, že jsi tuhle songu už smazal a nebo mi ji nikdy neposlal :)");
                    }
                    else
                    {
                        db.SongRecords.Remove(song);
                        await db.SaveChangesAsync();
                        await ReplyAsync("Ok, tahle songa už tady hrát nebude :)");
                    }
                }
                catch (DbUpdateException error)
                {
                    await ReplyAsync("500: Nebudu lhát, jsem špatnej programátor :(");
                }
            }
        }
    }
}