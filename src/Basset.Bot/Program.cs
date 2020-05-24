using System.Threading.Tasks;

namespace Basset.Bot
{
    class Program
    {
        static Task Main(string[] args)
            => new Startup(args).StartAsync();
    }
}
