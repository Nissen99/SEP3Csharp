using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Songs;
using Entities;

namespace RestT2_T3
{
    public class SongManageRestClient : ISongManageNetworking
    {
        private string uri = "http://localhost:8080/";

        public async Task AddNewSongAsync(Song newSong)
        {
            using HttpClient httpClient = new HttpClient();

            Console.WriteLine($"(REST CLIENT Title: {newSong.Title}");

            string newSongAsJson = JsonSerializer.Serialize(newSong,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            
            StringContent newSongAsStringContent = new StringContent(newSongAsJson, Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = await httpClient.PostAsync(uri + "/song", newSongAsStringContent);
            
            RequestCodeCheck(responseMessage);
            
        }
        
        protected static void RequestCodeCheck(HttpResponseMessage responseMessage)
        {
            Console.WriteLine("Checking request");
            if (!responseMessage.IsSuccessStatusCode)
            {
                Console.WriteLine("Not good");
                throw new Exception($"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
            }
        }
        
    }
    }