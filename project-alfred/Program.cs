using System.Threading.Tasks;
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
            
            var services = new ServiceCollection();
            services.AddSingleton(config);
            services.AddSingleton<BootService>();

            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<BootService>().Boot();
            
            await Task.Delay(-1);
        }
    }
}