using Basset.Data;
using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace Basset.Bot.Commands
{
    [Group("guild")]
    public class GuildConfigModule : BotModuleBase
    {
        private readonly RootDatabase _db;

        public GuildConfigModule(RootDatabase db)
        {
            _db = db;
        }

        [Command]
        public async Task GetGuildInfoAsync()
        {
            // Display guild's non-sensitive configuration options
            await Task.Delay(0);
        }

        [Command("roleweight")]
        public async Task GetRoleWeightAsync(IRole role)
        {
            // Display a role's weight for playlist generation
            await Task.Delay(0);
        }

        [Command("roleweight")]
        public async Task SetRoleWeightAsync(IRole role, double weight)
        {
            // Set a role's weight for playlist generation
            await Task.Delay(0);
        }

        [Command("userweight")]
        public async Task GetUserWeightAsync(IUser user)
        {
            // Display a user's weight for playlist generation
            await Task.Delay(0);
        }

        [Command("userweight")]
        public async Task SetUserWeightAsync(IUser user, double weight)
        {
            // Set a user's weight for playlist generation
            await Task.Delay(0);
        }

        [Command("featureweight")]
        public async Task GetFeatureWeightAsync(string name)
        {
            // Display all feature weights
            await Task.Delay(0);
        }

        [Command("featureweight")]
        public async Task SetFeatureWeightAsync(object options)
        {
            // Set one or many feature's weights
            await Task.Delay(0);
        }
    }
}
