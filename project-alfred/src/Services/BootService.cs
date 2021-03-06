using System;
using System.Net;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace project_alfred.Services
{
    public class BootService
    {
        private readonly IConfigurationRoot _config;
        private readonly DiscordSocketClient _client;
        
        public BootService(DiscordSocketClient client, IConfigurationRoot config)
        {
            _config = config;
            _client = client;
        }
        
        public async void Boot()
        {
            var token = _config["token"];

            try
            {
                await _client.LoginAsync(TokenType.Bot, "token");
                await _client.StartAsync();
            }
            catch (Discord.Net.HttpException error)
            {
                switch (error.HttpCode)
                {
                    case HttpStatusCode.Unauthorized:
                        Console.Write($@"Passed bot token is malformed/invalid.");
                        break;
                    default:
                        Console.Write($@"Connection to discord bot via token has failed, error message:
{error.Message}");
                        break;
                }
                
                Environment.Exit(1);
            }
            catch (Exception error)
            {
                Console.Write($@"General Exception has occured: 
{error.Message}");
                
                Environment.Exit(1);
            }
        }
    }
}