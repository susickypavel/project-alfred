using System;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;

namespace project_alfred.Services
{
    public class BootService
    {
        private readonly DiscordSocketClient _client;
        private readonly LoggerService _logger;

        public BootService(DiscordSocketClient client, LoggerService logger)
        {
            this._client = client;
            this._logger = logger;

            this._client.Log += _logger.Log;
        }

        public async Task Start()
        {
            try
            {
                var token = "token";
            
                await _client.LoginAsync(TokenType.Bot, token);
                await _client.StartAsync();
            }
            catch (Exception error)
            {
                await _logger.Log(new LogMessage(LogSeverity.Error, "BootService", "Could not connect to bot, see error below:", error));
            }
        }
    }
}