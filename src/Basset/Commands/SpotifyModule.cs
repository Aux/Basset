using Basset.Spotify;
using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Basset.Commands
{
    public class SpotifyModule : BotModuleBase
    {
        [Command("genres")]
        [Remarks("Get a list of all available genres on Spotify")]
        public async Task GenresAsync()
        {
            var genrePath = Path.Combine(AppContext.BaseDirectory, "spotify_genres.json");
            var genres = JsonConvert.DeserializeObject<List<string>>(genrePath);
            await ReplyEmbedAsync(new EmbedBuilder()
                .WithTitle("Available Genres")
                .WithDescription(string.Join(", ", genres)));
        }
    }
}
