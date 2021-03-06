using System.Threading.Tasks;
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
                .AddSingleton<BootService>();

            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<LogService>();
            serviceProvider.GetService<BootService>().Boot();
            
            await Task.Delay(-1);
        }
    }
}