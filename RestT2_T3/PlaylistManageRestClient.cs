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
            HttpResponseMessage responseMessage = await client.PostAsync(Uri + $"playlistManage/{playlist.Id}/", content);
            HandleResponseNoReturn(responseMessage);
        }

        public async Task RemoveSongFromPlaylistAsync(Playlist playlist, Song song)
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage responseMessage = await client.DeleteAsync(Uri + $"playlistManage/{playlist.Id}/{song.Id}");
            HandleResponseNoReturn(responseMessage);

        }

    
    }
}