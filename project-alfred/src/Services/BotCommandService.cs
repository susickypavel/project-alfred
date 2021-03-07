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
            // register modules that are public and inherit ModuleBase<T>.
            await _command.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }
        
        private async Task OnMessageReceived(SocketMessage rawMessage)
        {
            // ensures we don't process system/other bot messages
            if (!(rawMessage is SocketUserMessage message)) 
            {
                return;
            }
            
            if (message.Source != MessageSource.User) 
            {
                return;
            }

            // sets the argument position away from the prefix we set
            var argPos = 0;

            // get prefix from the configuration file
            var prefix = char.Parse("+");

            // determine if the message has a valid prefix, and adjust argPos based on prefix
            if (!(message.HasMentionPrefix(_client.CurrentUser, ref argPos) || message.HasCharPrefix(prefix, ref argPos))) 
            {
                return;
            }
           
            var context = new SocketCommandContext(_client, message);

            // execute command if one is found that matches
            await _command.ExecuteAsync(context, argPos, _services);     
        }

        private async Task OnCommandExecuted(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            // if a command isn't found, log that info to console and exit this method
            if (!command.IsSpecified) {
                System.Console.WriteLine($"Command failed to execute for [] <-> []!");
                return;
            }

            // log success to the console and exit this method
            if (result.IsSuccess) {
                System.Console.WriteLine($"Command [] executed for -> []");
                return;
            }
            
            // failure scenario, let's let the user know
            await context.Channel.SendMessageAsync($"Sorry, ... something went wrong -> []!");
        }
    }
}