using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

using project_alfred.Services;

namespace project_alfred
{
    internal class Program
    {
        public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {
            var services = new ServiceCollection();
            services.AddSingleton<BootService>();

            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<BootService>().Boot();
        }
    }
}