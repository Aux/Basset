using System.Threading.Tasks;

namespace Basset
{
    class Program
    {
        static Task Main(string[] args)
            => new Startup(args).RunAsync();
    }
}
