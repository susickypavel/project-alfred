using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using project_alfred.logger;
using project_alfred.utils;

namespace project_alfred
{
    internal class Program
    {
        private readonly Logger _logger = new Logger();
        
        private DiscordSocketClient _client;
        
        private static Task Main(string[] args) => new Program().MainAsync();

        private async Task MainAsync()
        {
            var credentialsConfiguration = new CredentialsConfiguration();
            
            new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build()
                    .Bind(credentialsConfiguration);
            
            _client = new DiscordSocketClient();
            _client.Log += _logger.Log;
            
            await _client.LoginAsync(TokenType.Bot, credentialsConfiguration.DISCORD_BOT_TOKEN);
            await _client.StartAsync();
            
            await Task.Delay(-1);
        }
    }
}