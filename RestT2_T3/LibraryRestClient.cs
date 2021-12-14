using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Library;
using Entities;
/*
 * Denne klasse står REST kommunikationen af musik biblioteket
 */
namespace RestT2_T3
{
    public class LibraryRestClient : HttpClientBase, ILibraryNetworking
    {
        public async Task<IList<Song>> GetAllSongsAsync()
        {
            
                using HttpClient client = new HttpClient();
            
                HttpResponseMessage responseMessage = await client.GetAsync(Uri + "song");

                return await HandleResponseGet<IList<Song>>(responseMessage);
                

        }
    }
}