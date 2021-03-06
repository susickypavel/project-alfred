using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace project_alfred.Services
{
    public class BootService
    {
        private readonly IConfigurationRoot _config;
        private readonly DiscordSocketClient _client;
        
        public BootService(IServiceProvider provider)
        {
            _config = provider.GetRequiredService<IConfigurationRoot>();
            // TODO: extract to service
            _client = new DiscordSocketClient();
        }
        
        public async void Boot()
        {
            _client.Log += Log;
            
            var token = _config["token"];
            
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
        }
        
        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}