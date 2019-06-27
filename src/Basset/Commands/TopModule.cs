using Basset.Data;
using Basset.Spotify;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basset.Commands
{
    [Group("top")]
    public class TopModule : BotModuleBase
    {
        private readonly SpotifyDatabase _db;

        public TopModule(SpotifyDatabase db)
        {
            _db = db;
        }

        [Priority(1)]
        [Command("tracks"), Alias("t")]
        [Remarks("View the top 10 tracks in this guild")]
        public async Task TopTracksAsync()
        {
            var tracks = _db.Listens.Where(x => x.GuildId == Context.Guild.Id).ToList()
                .GroupBy(x => x.TrackId)
                .OrderByDescending(x => x.Count())
                .Take(10);

            var builder = new StringBuilder();
            foreach (var item in tracks)
            {
                var track = _db.Tracks.FirstOrDefault(x => x.Id == item.Key);
                builder.AppendLine($"**{item.Count()} listens:** [{track.Artists} - {track.TrackTitle}]({string.Format(SpotifyConstants.TrackUrlFormat, track.Id)})");
            }

            await ReplyEmbedAsync(new EmbedBuilder()
                .WithTitle("Top 10 Tracks")
                .WithDescription(builder.ToString()));
        }

        [Priority(1)]
        [Command("listeners"), Alias("l", "listens", "users")]
        [Remarks("View the top 10 listeners in this guild")]
        public async Task TopListenersAsync()
        {
            var users = _db.Listens.Where(x => x.GuildId == Context.Guild.Id).ToList()
                .GroupBy(x => x.UserId)
                .OrderByDescending(x => x.Count())
                .Take(10);

            var builder = new StringBuilder();
            foreach (var item in users)
                builder.AppendLine($"{MentionUtils.MentionUser(item.Key)} with **{item.Count()}** listens over **{item.GroupBy(x => x.TrackId).Count()}** tracks");
            
            await ReplyEmbedAsync(new EmbedBuilder()
                .WithTitle("Top 10 Listeners")
                .WithDescription(builder.ToString()));
        }

        [Priority(2)]
        [Command]
        [Remarks("View a user's top 10 tracks")]
        public async Task UserTopAsync([Remainder]SocketGuildUser user = null)
        {
            user = user == null ? Context.User as SocketGuildUser : user;
            var listens = _db.Listens.Where(x => x.GuildId == Context.Guild.Id && x.UserId == user.Id).ToList()
                .GroupBy(x => x.TrackId)
                .OrderByDescending(x => x.Count())
                .Take(10);

            if (listens.Count() == 0)
            {
                await ReplyAsync("This user has not listened to any music recently");
                return;
            }

            var builder = new StringBuilder();
            foreach (var item in listens)
            {
                var track = _db.Tracks.FirstOrDefault(x => x.Id == item.Key);
                builder.AppendLine($"**{item.Count()} listens:** [{track.Artists} - {track.TrackTitle}]({string.Format(SpotifyConstants.TrackUrlFormat, track.Id)})");
            }

            await ReplyEmbedAsync(new EmbedBuilder()
                .WithTitle($"{user.ToString()}'s Top 10 Tracks")
                .WithDescription(builder.ToString()));
        }
    }
}
