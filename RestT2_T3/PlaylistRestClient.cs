using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Playlist;
using Entities;

namespace RestT2_T3
{
    public class PlaylistRestClient : IPlaylistNetworking
    {
        private string uri = "http://localhost:8080/";
        public async Task<Playlist> CreatePlaylistAsync(Playlist playlist, User user)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Playlist>> GetAllPlaylistsForUserAsync(User user)
        {
            using HttpClient client = new HttpClient();
            string stringAsync = await client.GetStringAsync(uri + "playlist/" + user.Username);

            Console.WriteLine(stringAsync);
            return JsonSerializer.Deserialize<IList<Playlist>>(stringAsync,
                new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
        }
        public async Task<IList<Song>> GetAllSongsFromPlaylistAsync(Playlist playlist)
        {
            using HttpClient client = new HttpClient();
            string stringAsync = await client.GetStringAsync(uri + "playlistSongs/" + playlist.Id);

            Console.WriteLine(stringAsync);
            return JsonSerializer.Deserialize<IList<Song>>(stringAsync,
                new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
        }

        public async Task DeletePlayListAsync(Playlist playlist)
        {
            throw new NotImplementedException();
        }
    }
}