using System;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.PlaylistManage;
using Entities;

namespace RestT2_T3
{
    public class PlaylistManageRestClient : HttpClientBase, IPlaylistMangeNetworking
    {
        public async Task AddSongToPlaylistAsync(Playlist playlist, Song song)
        {
            using HttpClient client = new HttpClient();
            StringContent content = FromObjectToStringContentCamelCase(song);
            HttpResponseMessage responseMessage = await client.PostAsync(Uri + $"playlist/{playlist.Id}/", content);
            HandleResponsePostAndRemove(responseMessage);
        }

        public async Task RemoveSongFromPlaylistAsync(Playlist playlist, Song song)
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage responseMessage = await client.DeleteAsync(Uri + $"playlist/{playlist.Id}/{song.Id}");
            HandleResponsePostAndRemove(responseMessage);

        }
    }
}