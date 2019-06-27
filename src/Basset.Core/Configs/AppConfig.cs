namespace Basset
{
    public class AppConfig
    {
        public AppConfig()
        {
            Logging = new LoggingConfig();
        }

        public string DiscordToken { get; set; } = "";
        public string DefaultSpotifyToken { get; set; } = "";
        public LoggingConfig Logging { get; set; }
    }
}
