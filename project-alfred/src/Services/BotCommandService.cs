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
    public class BotCommandService
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _command;
        private readonly IServiceProvider _services;
        
        public BotCommandService(IServiceProvider services, DiscordSocketClient client, CommandService command)
        {
            _services = services;
            _client = client;
            _command = command;

            _client.MessageReceived += OnMessageReceived;
            _command.CommandExecuted += OnCommandExecuted;
        }

        public async Task InitializeAsync()
        {
            _command.AddTypeReader(typeof(Url), new UrlTypeReader());
            await _command.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }
        
        private async Task OnMessageReceived(SocketMessage rawMessage)
        {
            if (!(rawMessage is SocketUserMessage message)) {
                return;
            }
            
            if (message.Source != MessageSource.User) {
                return;
            }

            var argPos = 0;

            var prefix = char.Parse("+");

            if (!(message.HasMentionPrefix(_client.CurrentUser, ref argPos) || message.HasCharPrefix(prefix, ref argPos))) {
                return;
            }
           
            var context = new SocketCommandContext(_client, message);

            await _command.ExecuteAsync(context, argPos, _services);     
        }

        private async Task OnCommandExecuted(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            switch (result.Error)
            {
                case CommandError.UnknownCommand:
                    await context.Channel.SendMessageAsync("To neumím");
                    break;
                case CommandError.ParseFailed:
                    await context.Channel.SendMessageAsync(result.ErrorReason);
                    break;
                case CommandError.BadArgCount:
                    await context.Channel.SendMessageAsync(GetCommandSyntax(command));
                    break;
                case CommandError.ObjectNotFound:
                    break;
                case CommandError.MultipleMatches:
                    await context.Channel.SendMessageAsync("LOL");
                    break;
                case CommandError.UnmetPrecondition:
                    break;
                case CommandError.Exception:
                    break;
                case CommandError.Unsuccessful:
                    break;
                case null:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private string GetCommandSyntax(Optional<CommandInfo> command)
        {
            var builder = new StringBuilder($"Zkus to takhle: `+{command.Value.Name}");
            
            foreach (var parameter in command.Value.Parameters)
            {
                builder.Append($" <{parameter.Name}>");
            }

            builder.Append("`");

            return builder.ToString();
        }
    }
}