using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Songs;
using Entities;

namespace RestT2_T3
{
    public class SongsRestClient : HttpClientBase, ISongsNetworking
    {
        public async Task<IList<Song>> GetAllSongs()
        {
            using HttpClient client = new HttpClient();
            
            HttpResponseMessage responseMessage = await client.GetAsync(Uri + "songs");

            return await HandleResponseGet<IList<Song>>(responseMessage);
            
        }
    }
}