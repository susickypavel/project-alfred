using System;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

using project_alfred.TypeReaders;

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
            
            _command.AddTypeReader(typeof(Url), new UrlTypeReader());
            await _command.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        private async Task OnCommandExecuted(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            var commandName = command.IsSpecified ? command.Value.Name : "A command";
            var severity = result.Error != null ? LogSeverity.Error : LogSeverity.Info;
            
            await _logger.Log(new LogMessage(severity, "CommandExecution", $"{commandName} was executed at {DateTime.UtcNow}"));
            
            if (!result.IsSuccess)
            {
                var message = new StringBuilder($"{context.User.Id} ({context.User.Username}#{context.User.DiscriminatorValue}) executed command {commandName}: ");
                
                switch (result.Error)
                {
                    case CommandError.UnknownCommand:
                        message.Append($"Such command does not exist.");
                        break;
                    case CommandError.ParseFailed:
                        message.Append($"Wrong parameter value was send.");
                        break;
                    case CommandError.BadArgCount:
                        message.Append($"Wrong count of parameters was send.");
                        break;
                    case CommandError.ObjectNotFound:
                        break;
                    case CommandError.MultipleMatches:
                        break;
                    case CommandError.UnmetPrecondition:
                        break;
                    case CommandError.Exception:
                        break;
                    case CommandError.Unsuccessful:
                        message.Append($"Something went wrong.");
                        break;
                }
                
                await _logger.Log(new LogMessage(LogSeverity.Debug, "CommandExecution", message.ToString()));
            }
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