using System;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace project_alfred.Services
{
    public class BootService
    {
        private readonly DiscordSocketClient _client;
        private readonly LoggerService _logger;
        private readonly IConfigurationRoot _config;
        private readonly CommandService _command;

        public BootService(DiscordSocketClient client, LoggerService logger, IConfigurationRoot config, CommandService command)
        {
            this._client = client;
            this._logger = logger;
            this._config = config;
            this._command = command;

            this._client.Log += _logger.Log;
            this._command.Log += _logger.Log;
        }

        public async Task Start()
        {
            try
            {
                await _client.LoginAsync(TokenType.Bot, _config["DISCORD_TOKEN"]);
                await _client.StartAsync();
            }
            catch (Exception error)
            {
                await _logger.Log(new LogMessage(LogSeverity.Error, "BootService", "Could not connect to bot, see error below:", error));
            }
        }
    }
}