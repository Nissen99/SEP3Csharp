using System;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Play;
using Entities;

namespace RestT2_T3
{
    public class PlayRestClient : IPlayNetworking
    {
        private string uri = "http://localhost:8080/";
        public async Task<string> GetSongWithMP3(Song song)
        {
            using HttpClient client = new HttpClient();
            Task<string> stringAsync = client.GetStringAsync(uri + $"songs/{song.Id}");
            Console.WriteLine(stringAsync.Result.Length);
            return await stringAsync;
        }

        public async Task<string> GetAllSongs()
        {
            using HttpClient client = new HttpClient();
            Task<string> stringAsync = client.GetStringAsync(uri + "songs");
            
            return await stringAsync;
        }
    }
}