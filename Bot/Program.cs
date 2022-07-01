using Discord;
using Discord.Commands;
using Domain.Context;
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
            services.AddSingleton(new CommandService(new CommandServiceConfig()
            {
                CaseSensitiveCommands = false,
                LogLevel = LogSeverity.Info,
            }));
            services.AddDbContext<ProjectAlfredContext>();
        })
    .Build();

await host.RunAsync();