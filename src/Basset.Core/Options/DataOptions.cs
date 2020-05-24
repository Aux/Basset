namespace Basset.Options
{
    public enum ServerType
    {
        SQLite,
        MySQL,
        Postgres    // Not implemented
    }

    public class DataOptions
    {
        public ServerType ServerType { get; set; } = ServerType.SQLite;
        public string Database { get; set; } = "basset";
        public string Host { get; set; } = "localhost";
        public int Port { get; set; } = 3306;
        public string User { get; set; } = "root";
        public string Password { get; set; } = null;
    }
}
