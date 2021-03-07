using System;
using System.Reflection;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

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
            if (!command.IsSpecified) {
                System.Console.WriteLine($@"[Fail] - Command {context.Message} has failed.");
                return;
            }

            if (result.IsSuccess) {
                System.Console.WriteLine($@"[Success] - Command {context.Message} executed.");
                return;
            }
            
            await context.Channel.SendMessageAsync($"Sorry kámo, něco se totálně posralo.");
        }
    }
}