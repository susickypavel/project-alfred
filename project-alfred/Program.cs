using System;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace project_alfred
{
    class Program
    {
        private DiscordSocketClient _client;
        private IConfiguration _config;
        
        public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        public Program()
        {
            _client = _client = new DiscordSocketClient();
            _client.Log += Log;

            _config = new ConfigurationBuilder()
                .AddJsonFile("config.json").Build();
        }
        
        public async Task MainAsync()
        {
            var token = _config["token"];
            
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }
        
        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}