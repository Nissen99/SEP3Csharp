using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Album;
using Entities;

namespace RestT2_T3
{
    public class AlbumRestClient : IAlbumNetworking
    {
        private string uri = "http://localhost:8080/";

        public async Task<IList<Album>> SearchForAlbums(string title)
        {
            using HttpClient client = new HttpClient();
            string responseFromServerAsJson = await client.GetStringAsync(uri + $"album/{title}");

            IList<Album> albumsFromServer = JsonSerializer.Deserialize<IList<Album>>(responseFromServerAsJson,
                new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });


            return albumsFromServer;
        }
    }
}