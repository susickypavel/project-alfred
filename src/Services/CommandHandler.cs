using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace project_alfred.Services
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _command;
        private readonly IServiceProvider _services;
        private readonly LoggerService _logger;

        public CommandHandler(DiscordSocketClient client, CommandService command, IServiceProvider services, LoggerService logger)
        {
            this._client = client;
            this._command = command;
            this._services = services;
            this._logger = logger;
        }

        public async Task InstallCommandsAsync()
        {
            _client.MessageReceived += OnMessageReceived;
            _command.CommandExecuted += OnCommandExecuted;
            
            await _command.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        private async Task OnCommandExecuted(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            var commandName = command.IsSpecified ? command.Value.Name : "A command";
            await _logger.Log(new LogMessage(LogSeverity.Info, "CommandExecution", $"{commandName} was executed at {DateTime.UtcNow}"));
        }

        private async Task OnMessageReceived(SocketMessage msg)
        {
            var message = msg as SocketUserMessage;
            
            if (message == null) return;

            var argPos = 0;

            if (!(message.HasCharPrefix('+', ref argPos) || 
                  message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
                message.Author.IsBot)
                return;

            var context = new SocketCommandContext(_client, message);
            
            await _command.ExecuteAsync(
                context: context, 
                argPos: argPos,
                services: _services);
        }
    }
}