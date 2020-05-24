using Basset.Data;
using Discord.Commands;
using System.Threading.Tasks;

namespace Basset.Bot.Commands
{
    public class UserConfigModule : BotModuleBase
    {
        private readonly RootDatabase _db;

        public UserConfigModule(RootDatabase db)
        {
            _db = db;
        }

        [Command("optout")]
        public async Task OptOutAsync()
        {
            await Task.Delay(0);
        }

        [Command("optin")]
        public async Task OptInAsync()
        {
            await Task.Delay(0);
        }
    }
}
