using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Songs;
using Entities;

namespace RestT2_T3
{
    public class SongsRestClient : ISongsNetworking
    {
        private string uri = "http://localhost:8080/";
        public async Task<IList<Song>> GetAllSongs()
        {
            using HttpClient client = new HttpClient();
            string stringAsync = await client.GetStringAsync(uri + "songs");

            Console.WriteLine(stringAsync);
            return JsonSerializer.Deserialize<IList<Song>>(stringAsync,
                new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
        }
    }
}