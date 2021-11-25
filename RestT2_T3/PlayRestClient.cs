using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Play;
using Entities;
using RestT2_T3.Util;

namespace RestT2_T3
{
    public class PlayRestClient : IPlayNetworking
    {
        private string uri = "http://localhost:8080/";
        public async Task<Song> GetSongWithMP3(Song song)
        {
            using HttpClient client = new HttpClient();
            string stringAsync = await client.GetStringAsync(uri + $"songs/{song.Id}");
            
            Song songWithMP3 = JsonSerializer.Deserialize<Song>(stringAsync, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new ByteArrayConverter() }
            });
            
            return  songWithMP3;
        }

        public async Task<IList<Song>> GetAllSongs()
        {
            using HttpClient client = new HttpClient();
            string stringAsync = await client.GetStringAsync(uri + "songs");
            
            return JsonSerializer.Deserialize<IList<Song>>(stringAsync,
                new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
        }
    }
}