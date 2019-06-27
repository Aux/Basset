using Discord;
using Discord.Commands;
using Discord.Rest;
using System.IO;
using System.Threading.Tasks;

namespace Basset.Commands
{
    public class BotModuleBase : ModuleBase<ShardedCommandContext>
    {
        public Task<IUserMessage> ReplyEmbedAsync(Embed embed)
            => ReplyAsync("", false, embed, null);
        public Task<IUserMessage> ReplyEmbedAsync(EmbedBuilder builder)
            => ReplyAsync("", false, builder.Build(), null);

        public Task ReplyReactionAsync(IEmote emote)
            => Context.Message.AddReactionAsync(emote);

        public Task<RestUserMessage> ReplyFileAsync(Stream stream, string fileName, string message = null)
            => Context.Channel.SendFileAsync(stream, fileName, message, false, null);
    }
}
