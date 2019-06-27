namespace Basset
{
    public class AppConfig
    {
        public AppConfig()
        {
            Logging = new LoggingConfig();
            Web = new WebConfig();
        }

        public string DiscordToken { get; set; } = "";
        public string SpotifyToken { get; set; } = "";
        public LoggingConfig Logging { get; set; }
        public WebConfig Web { get; set; }
    }
}
