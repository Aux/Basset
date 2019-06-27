using Basset.Data;
using Basset.Spotify;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Basset.Commands
{
    [Group("random")]
    public class RandomModule : BotModuleBase
    {
        private readonly SpotifyDatabase _db;

        public RandomModule(SpotifyDatabase db)
        {
            _db = db;
        }

        [Command("track"), Alias("song")]
        public async Task RandomTrack([Remainder]SocketGuildUser user = null)
        {
            var tracks = user == null ?
                _db.Listens.Where(x => x.GuildId == Context.Guild.Id).Select(x => x.TrackId).ToList() :
                _db.Listens.Where(x => x.UserId == Context.User.Id).Select(x => x.TrackId).ToList();

            int selected = new Random().Next(0, tracks.Count());
            var trackId = tracks.ElementAtOrDefault(selected);
            await ReplyAsync(string.Format(SpotifyConstants.TrackUrlFormat, trackId));
        }
    }
}
