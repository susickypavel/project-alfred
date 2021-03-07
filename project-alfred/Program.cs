using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using project_alfred.Services;

namespace project_alfred
{
    internal class Program
    {
        public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("config.json").Build();

            var client = new DiscordSocketClient();

            var services = new ServiceCollection()
                .AddSingleton(config)
                .AddSingleton(client)
                .AddSingleton<LogService>()
                .AddSingleton<BootService>()
                .AddSingleton<CommandService>()
                .AddSingleton<BotCommandService>();

            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetRequiredService<LogService>();
            serviceProvider.GetRequiredService<BootService>().Boot();
            await serviceProvider.GetRequiredService<BotCommandService>().InitializeAsync();
            
            await Task.Delay(-1);
        }
    }
}