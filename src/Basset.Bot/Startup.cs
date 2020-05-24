using Basset.Bot.Services;
using Basset.Data;
using Basset.Logging;
using Basset.Services;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Basset.Bot
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(string[] args)
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory, "common"))
                .AddYamlFile("config.yml")
                .AddCommandLine(args)
                .Build();
        }

        public async Task StartAsync()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            var provider = services.BuildServiceProvider();

            var discord = provider.GetRequiredService<DiscordShardedClient>();
            await discord.LoginAsync(TokenType.Bot, _config["discord:token"]);
            await discord.StartAsync();

            var commands = provider.GetRequiredService<CommandService>();
            await commands.AddModulesAsync(Assembly.GetExecutingAssembly(), provider);

            provider.GetRequiredService<ILoggerFactory>().AddProvider(new BotLoggerProvider(_config));
            provider.GetRequiredService<LoggingService>().Start();
            provider.GetRequiredService<CommandHandlingService>().Start();

            await Task.Delay(-1);
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services
                .AddSingleton(new DiscordShardedClient(new DiscordSocketConfig
                {
                    LogLevel = LogSeverity.Verbose
                }))
                .AddSingleton(new CommandService(new CommandServiceConfig
                {
                    CaseSensitiveCommands = false,
                    IgnoreExtraArgs = false
                }))
                .AddSingleton(_config)
                .AddSingleton<LoggingService>()
                .AddSingleton<CommandHandlingService>()
                .AddDbContext<RootDatabase>()
                .AddLogging();
        }
    }
}
