using RestEase;
using System.Threading.Tasks;

namespace Basset.Spotify
{
    [Header("Accept", "application/json")]
    [Header("Content-Type", "application/json")]
    public interface ISpotifyApi
    {
        [Get("recommendations")]
        Task<SpotifyRecommendedResponse> GetRecommendationsAsync(
            [Header("Authorization")]string authorization, [QueryMap]GetRecommendationsParams args);

        [Post("users/{userId}/playlists")]
        Task<object> CreatePlaylistAsync(
            [Header("Authorization")]string authorization,
            [Path]string userId, 
            [Body]CreatePlaylistParams args);

        [Post("playlists/{playlistId}/tracks")]
        Task<object> AddPlaylistTracksAsync(
            [Header("Authorization")]string authorization,
            [Path]string playlistId, 
            [Body]AddPlaylistTracksParams args);

        [Put("playlists/{playlistId}")]
        Task<object> ModifyPlaylistAsync(
            [Header("Authorization")]string authorization,
            [Path]string playlistId, 
            [Body]ModifyPlaylistParams args);

        [Put("playlists/{playlistId}/images")]
        Task UploadPlaylistImageAsync(
            [Header("Authorization")]string authorization,
            [Path]string playlistId, 
            [Body]string base64jpg);
    }
}
