using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Discord.Commands;

using project_alfred.models;

namespace project_alfred.Modules
{
    public class QueryCommands : ModuleBase<SocketCommandContext>
    {
        [Command("leaderboard")]
        public async Task ListSongs()
        {
            var stringBuilder = new StringBuilder("```\n\r");
            stringBuilder.AppendLine("+--------------------+--------+");

            await using (var context = new SongRecordContext())
            {
                var temp = context.SongRecords
                    .AsQueryable()
                    .GroupBy(song => song.user)
                    .Select(g => new { name = g.Key, count = g.Count() });

                foreach (var VARIABLE in temp)
                {
                    stringBuilder.AppendLine($"|{VARIABLE.name,20}|{VARIABLE.count,8}|");
                    stringBuilder.AppendLine("+--------------------+--------+");
                }
            }

            stringBuilder.Append("```");
            
            await ReplyAsync(stringBuilder.ToString());
        }
    }
}