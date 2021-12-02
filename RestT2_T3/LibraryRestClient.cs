using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Library;
using Entities;

namespace RestT2_T3
{
    public class LibraryRestClient : ILibraryNetworking
    {
        private string uri = "http://localhost:8080/";
        public async Task PostAllSongs(IList<Song> songList)
        {
            using HttpClient client = new HttpClient();
            string songListAsJson = JsonSerializer.Serialize(songList,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            StringContent content = new StringContent(songListAsJson, Encoding.UTF8, "application/json");

            Console.WriteLine("Yike");
            HttpResponseMessage response = await client.PostAsync(uri + "songs", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($@"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }

            Console.WriteLine("Done");
        }

        public async Task<IList<byte[]>> GetAllMP3()
        {
            using HttpClient client = new HttpClient();
            int count = 0;
            IList<byte[]> toReturn = new List<byte[]>();
            while (true)
            {
                try
                {
                    byte[] byteArrayAsync = await client.GetByteArrayAsync(uri + "mp3/" + count++);
                    toReturn.Add(byteArrayAsync);
                }
                catch (Exception e)
                {
                    break;
                }
            }

            return toReturn;
        }
    }
}