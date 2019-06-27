using Basset.Data;
using Basset.Spotify;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basset.Commands
{
    [Group("recommend")]
    public class RecommendationModule : BotModuleBase
    {
        private readonly SpotifyDatabase _db;
        private readonly ISpotifyApi _api;
        private readonly IConfiguration _config;

        public RecommendationModule(SpotifyDatabase db, ISpotifyApi api, IConfiguration config)
        {
            _db = db;
            _api = api;
            _config = config;
        }

        [Command]
        public async Task GuildRecommendAsync()
        {
            var tracks = _db.Listens.Where(x => x.GuildId == Context.Guild.Id).ToList()
                   .GroupBy(x => x.TrackId)
                   .OrderByDescending(x => x.Count())
                   .Take(5);

            await RecommendAsync(tracks: tracks.Select(x => x.Key).ToArray());
        }

        [Command("user")]
        public async Task UserRecommendAsync([Remainder]SocketGuildUser user = null)
        {
            user = user == null ? Context.User as SocketGuildUser : user;

            var tracks = _db.Listens.Where(x => x.GuildId == Context.Guild.Id && x.UserId == user.Id).ToList()
                .GroupBy(x => x.TrackId)
                .OrderByDescending(x => x.Count())
                .Take(5);

            await RecommendAsync(tracks: tracks.Select(x => x.Key).ToArray());
        }

        [Command("tracks")]
        public Task RecommendTracksAsync(params string[] tracks)
        {
            if (tracks.Length > 4 || tracks.Length == 0)
                return ReplyAsync("Between 1 and 5 tracks must be specified");
            else
                return RecommendAsync(tracks:tracks);
        }

        [Command("artists")]
        public Task RecommendArtistsAsync(params string[] artists)
        {
            if (artists.Length > 4 || artists.Length == 0)
                return ReplyAsync("Between 1 and 5 artists must be specified");
            else
                return RecommendAsync(artists: artists);
        }

        [Command("genres")]
        public Task RecommendGenresAsync(params string[] genres)
        {
            if (genres.Length > 4 || genres.Length == 0)
                return ReplyAsync("Between 1 and 5 genres must be specified");
            else
                return RecommendAsync(genres: genres);
        }

        private async Task RecommendAsync(string[] tracks = null, string[] artists = null, string[] genres = null)
        {
            var args = new GetRecommendationsParams
            {
                Limit = 10,
                SeedArtists = artists?.ToList(),
                SeedTracks = tracks?.ToList(),
                SeedGenres = genres?.ToList()
            };

            var response = await _api.GetRecommendationsAsync(_config["spotify_token"], args);

            var builder = new StringBuilder();
            foreach (var track in response.Tracks)
                builder.AppendLine($"[{string.Join(", ", track.Artists.Select(x => x.Name))} - {track.Name}]({string.Format(SpotifyConstants.TrackUrlFormat, track.Id)})");

            await ReplyEmbedAsync(new EmbedBuilder()
                .WithTitle("Spotify Recommends")
                .WithDescription(builder.ToString()));
        }
    }
}
