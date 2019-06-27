using Basset.Data;
using Basset.Services;
using Basset.Spotify;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestEase;
using System;
using System.IO;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Basset
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(string[] args)
        {
            TryGenerateConfiguration();
            var builder = new ConfigurationBuilder()
             .SetBasePath(AppContext.BaseDirectory)
             .AddYamlFile("_config.yml");
            Configuration = builder.Build();
        }

        public static bool TryGenerateConfiguration()
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, "_config.yml");
            if (File.Exists(filePath)) return false;

            var serializer = new SerializerBuilder()
                .WithNamingConvention(new UnderscoredNamingConvention())
                .Build();

            var yaml = serializer.Serialize(new AppConfig());
            File.WriteAllText(filePath, yaml);
            return true;
        }

        public async Task RunAsync()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            var provider = services.BuildServiceProvider();

            provider.GetRequiredService<StartupService>().Start();
            provider.GetRequiredService<LoggingService>().Start();
            provider.GetRequiredService<ActivityTrackingService>().Start();
            provider.GetRequiredService<CommandHandlingService>().Start();

            await Task.Delay(-1);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // Clients
            services.AddSingleton(new DiscordShardedClient(new DiscordSocketConfig
            {
                LogLevel = Discord.LogSeverity.Verbose,
                MessageCacheSize = 0
            }));
            services.AddSingleton(new CommandService(new CommandServiceConfig
            {
                DefaultRunMode = RunMode.Async,
                CaseSensitiveCommands = false,
                QuotationMarkAliasMap = null,
                LogLevel = Discord.LogSeverity.Verbose
            }));

            var spotify = RestClient.For<ISpotifyApi>(SpotifyConstants.ApiUrl);

            // Internal
            services.AddDbContext<RootDatabase>()
                .AddDbContext<SpotifyDatabase>()
                .AddSingleton<StartupService>()
                .AddSingleton<LoggingService>()
                .AddSingleton<CommandHandlingService>()
                .AddSingleton<ActivityTrackingService>()
                .AddSingleton(spotify);

            // Etc
            services.AddLogging()
                .AddSingleton(Configuration);
        }
    }
}
