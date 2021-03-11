using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

using Discord;
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
                url = url.Value,
                publishedAt = Context.Message.Timestamp.UtcDateTime
            };
            
            var context = new SongRecordContext();
            context.Add(std);
            await context.SaveChangesAsync();

            await ReplyAsync($"🚀 Song was added!");
        }

        // [Command("fetch")]
        // public Task FetchSongsFromChannel()
        // {
        //     return Task.Run(async () =>
        //     {
        //         ulong lastMessage = 0;
        //         var messages = new List<IMessage>();
        //     
        //         while (true)
        //         {
        //             var enumerator = Context.Channel
        //                 .GetMessagesAsync(lastMessage, Direction.Before, 50, CacheMode.AllowDownload).FlattenAsync().Result.ToList();
        //
        //             if (enumerator.Count == 0) {
        //                 break;
        //             }
        //        
        //             messages.AddRange(enumerator);
        //             lastMessage = enumerator.Last().Id;
        //         }
        //
        //         var url = new Url();
        //
        //         await using (var context = new SongRecordContext())
        //         {
        //             foreach (var message in messages)
        //             {
        //                 url.Value = message.Content;
        //
        //                 if (url.IsValid)
        //                 {
        //                     context.Add(new SongRecord()
        //                     {
        //                         url = message.Content,
        //                         user = message.Author.Id,
        //                     });
        //                 }
        //             }
        //
        //             await context.SaveChangesAsync();
        //         }
        //
        //         await ReplyAsync("Hotovo"); 
        //     });
        // }
    }
}