using Basset.Spotify;
using Discord;
using Discord.Commands;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basset.Commands
{
    [Group("search"), Alias("find", "query")]
    public class SearchModule : BotModuleBase
    {
        private readonly ISpotifyApi _api;
        private readonly IConfiguration _config;

        public SearchModule(ISpotifyApi api, IConfiguration config)
        {
            _config = config;
            _api = api;
        }

        [Command]
        [Remarks("Search query keywords and optional field filters and operators")]
        [Summary("https://developer.spotify.com/documentation/web-api/reference/search/search/#writing-a-query---guidelines")]
        public async Task SearchAsync(string type, [Remainder]string query)
        {
            var response = await _api.GetSearchAsync(_config["spotify_token"], new GetSearchParams
            {
                Type = type,
                Query = query
            });

            var embed = new EmbedBuilder()
                .WithTitle("Search Results");
            if (response.Tracks != null)
            {
                var builder = new StringBuilder();
                foreach (var track in response.Tracks.Items)
                    builder.AppendLine($"[{string.Join(", ", track.Artists.Select(x => x.Name))} - {track.Name}]({string.Format(SpotifyConstants.TrackUrlFormat, track.Id)})");
                embed.AddField("Tracks", builder.ToString());
            }
            if (response.Artists != null)
            {
                var builder = new StringBuilder();
                foreach (var artist in response.Artists.Items)
                    builder.AppendLine($"[{artist.Name}]({string.Format(SpotifyConstants.ArtistUrlFormat, artist.Id)})");
                embed.AddField("Artists", builder.ToString());
            }

            if (embed.Fields.Count == 0)
                embed.WithDescription("No results found");

            await ReplyEmbedAsync(embed);
        }
    }
}
