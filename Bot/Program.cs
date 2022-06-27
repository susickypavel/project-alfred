using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using project_alfred.Services;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(config =>
    {
        config.AddUserSecrets<Program>();
    })
    .ConfigureServices(
        (_, services) =>
        {
            services.AddHostedService<DiscordClientService>();
        })
    .Build();

await host.RunAsync();