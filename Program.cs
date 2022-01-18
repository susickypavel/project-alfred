using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using project_alfred.services;

namespace project_alfred
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var hostBuilder = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile("appsettings.json");
                    config.AddEnvironmentVariables();
                    
                    // NOTE:
                    // 
                    // if (args != null)
                    // {
                    //     config.AddCommandLine(args);
                    // }
                })
                .ConfigureLogging((context, logging) =>
                {
                    logging.AddSimpleConsole(c =>
                    {
                        c.TimestampFormat = "[dd.MM.yyyy HH:mm:ss] ";
                    });
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddHostedService<DiscordClient>();
                }).Build();

            await hostBuilder.RunAsync();
        }
    }
}