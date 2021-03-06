using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Album;
using Entities;
/*
 * Denne klasse står REST kommunikationen af Album relaterede requests
 */
namespace RestT2_T3
{
    public class AlbumRestClient : HttpClientBase, IAlbumNetworking
    {
        
        
        public async Task<IList<Album>> SearchForAlbums(string title)
        {
            using HttpClient client = new HttpClient();

            HttpResponseMessage responseMessage = await client.GetAsync(Uri + $"album?title={title}");

            return await HandleResponseGet<IList<Album>>(responseMessage);
            
        }

        public async Task<IList<Album>> GetAllAlbumsAsync()
        {
            using HttpClient client = new HttpClient();
            
            HttpResponseMessage responseMessage = await client.GetAsync(Uri + $"album");

            return await HandleResponseGet<IList<Album>>(responseMessage);
          
        }
    }
}