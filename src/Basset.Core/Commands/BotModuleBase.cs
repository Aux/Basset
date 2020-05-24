using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace Basset
{
    public abstract class BotModuleBase : ModuleBase<BotCommandContext>
    {
        public Task ReplyAsync(Embed embed, RequestOptions options = null)
            => ReplyAsync("", false, embed, options);
    }
}
