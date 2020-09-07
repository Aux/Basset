using Basset.Logging;
using Basset.Services;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Basset.Collector
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory, "common"))
                .AddYamlFile("config.yml")
                .AddCommandLine(args)
                .Build();

            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddProvider(new BotLoggerProvider(config));
            });
            var logger = loggerFactory.CreateLogger<CollectingService>();

            var discord = new DiscordShardedClient(new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.GuildPresences | GatewayIntents.Guilds,
                LogLevel = LogSeverity.Debug
            });

            var loggingService = new LoggingService(loggerFactory, discord, null);
            loggingService.Start();

            var collectingService = new CollectingService(logger, discord, config);
            await collectingService.RunAsync();
        }
    }
}
