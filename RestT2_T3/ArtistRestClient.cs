using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Artist;
using Entities;

namespace RestT2_T3
{
    public class ArtistRestClient : HttpClientBase, IArtistNetworking
    {

        public async Task<IList<Artist>> SearchForArtists(string name)
        {
            using HttpClient client = new HttpClient();

            HttpResponseMessage responseMessage = await client.GetAsync(Uri + $"artist?name={name}");

            return await HandleResponseGet<IList<Artist>>(responseMessage);
       
        }

        public async Task<IList<Artist>> GetAllArtistsAsync()
        {
            using HttpClient client = new HttpClient();
            
            HttpResponseMessage responseMessage = await client.GetAsync(Uri + $"artist");

            return await HandleResponseGet<IList<Artist>>(responseMessage);
 
        }
    }
}