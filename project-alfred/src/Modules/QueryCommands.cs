using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using project_alfred.models;

namespace project_alfred.Modules
{
    
    public class QueryCommands : ModuleBase<SocketCommandContext>
    {
        private readonly string[] _medals = { "🥇", "🥈", "🥉" };
        
        [Command("leaderboard")]
        public async Task LeaderboardCommand()
        {
            var embedBuilder = new EmbedBuilder
            {
                Color = Color.Gold,
                Title = "Leaderboard",
                Description = "Who shares the most?",
                Author = new EmbedAuthorBuilder
                {
                    Name = "Alfred",
                    IconUrl = "https://discord.com/assets/dd4dbc0016779df1378e7812eabaa04d.png"
                    // N2H: Custom Icon <3
                }
            };

            await using (var db = new SongRecordContext())
            {
                var leaders = db.SongRecords
                    .AsQueryable()
                    .GroupBy(song => song.user)
                    .OrderByDescending(group => group.Count())
                    .Select(group=> new {id = group.Key, count = group.Count() }).ToArray();

                for (var i = 0; i < leaders.Length; i++)
                {
                    var leader = leaders[i];
                    var user = Context.Guild.GetUser(leader.id);

                    embedBuilder.AddField($"{(i < _medals.Length ? _medals[i] : null)} {user.Username}#{user.DiscriminatorValue}", $"{leader.count}", true);
                }

                await ReplyAsync(null, false, embedBuilder.Build());
            }
        }
    }
}