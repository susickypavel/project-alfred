using Discord;
using Discord.WebSocket;
using project_alfred.logger;

namespace project_alfred
{
    internal class Program
    {
        private readonly Logger _logger = new Logger();
        
        private DiscordSocketClient _client;
        
        private static Task Main(string[] args) => new Program().MainAsync();

        private async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _client.Log += _logger.Log;

            const string token = "";

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            
            await Task.Delay(-1);
        }
    }
}