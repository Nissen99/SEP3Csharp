using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Artist;
using Entities;
using RestT2_T3.Util;

namespace RestT2_T3
{
    public class ArtistRestClient : IArtistNetworking
    {
        private string uri = "http://localhost:8080/";

        public async Task<IList<Artist>> SearchForArtists(string name)
        {
            using HttpClient client = new HttpClient();
            string responseFromServerAsJson = await client.GetStringAsync(uri + $"artist/{name}");

            IList<Artist> artistsFromServer = JsonSerializer.Deserialize<IList<Artist>>(responseFromServerAsJson,
                new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                });
           
            
            return  artistsFromServer;        
        }
    }
}