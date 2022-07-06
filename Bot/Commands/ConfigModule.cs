using Discord;
using Discord.Commands;
using Domain.Context;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace project_alfred.Commands;

[Group("config")]
public class ConfigModule : ModuleBase<SocketCommandContext>
{
    [Group("channel")]
    public class ChannelModule : ModuleBase<SocketCommandContext>
    {
        private readonly ILogger<ChannelModule> _logger;
        private readonly ProjectAlfredContext _context;

        public ChannelModule(ILogger<ChannelModule> logger, ProjectAlfredContext context)
        {
            _logger = logger;
            _context = context;
        }
        
        [Command("set")]
        [Summary("Assigns selected channel as Music channel")]
        public async Task AssignMusicChannel(IChannel channel)
        {
            // TODO: Assert ChannelType is Text
            // TODO: Send error message on wrong argument
            
            try
            {
                var storedChannel = _context.MusicChannels.SingleOrDefault(mc => mc.GuildId == Context.Guild.Id);
        
                if (storedChannel != null)
                {
                    storedChannel.MusicChannelId = channel.Id;
                    await ReplyAsync("New MusicChannel overwritten.");
                }
                else
                {
                    await _context.MusicChannels.AddAsync(new MusicChannel()
                    {
                        GuildId = Context.Guild.Id,
                        MusicChannelId = channel.Id
                    });
                    await ReplyAsync("MusicChannel assigned.");
                }

                _logger.LogInformation("Guild '{GuildId}' assigned MusicChannel to '{ChannelId}'", Context.Guild.Id, channel.Id);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError("{Message}", e.Message);
                await ReplyAsync("Something went wrong");
            }
        }

        [Command("get")]
        [Summary("Gets current channel")]
        public async Task GetMusicChannel()
        {
            try
            {
                var musicChannel = await _context.MusicChannels.SingleOrDefaultAsync(mc => mc.GuildId == Context.Guild.Id);
            
                if (musicChannel != null)
                {
                    var discordMusicChannel = Context.Guild.Channels.SingleOrDefault(c => c.Id == musicChannel.MusicChannelId);

                    if (discordMusicChannel == null)
                    {
                        _context.MusicChannels.Remove(musicChannel);
                        await ReplyAsync("Assigned channel doesn't exist anymore. Assign new channel.");
                    }
                    else
                    {
                        await ReplyAsync($"Channel: {MentionUtils.MentionChannel(discordMusicChannel.Id)}");
                    }
                }
                else
                {
                    await ReplyAsync("You have no channel assigned as Music Channel. Use !config channel set <CHANNEL>.");
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError("{Message}", e.Message);
                await ReplyAsync("Something went wrong");
            }
        }
    }
}