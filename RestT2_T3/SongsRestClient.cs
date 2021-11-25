using System.Net.Http;
using System.Threading.Tasks;
using Domain.Songs;

namespace RestT2_T3
{
    public class SongsRestClient : ISongsNetworking
    {
        private string uri = "http://localhost:8080/";
        public async Task<string> GetAllSongs()
        {
            using HttpClient client = new HttpClient();
            Task<string> stringAsync = client.GetStringAsync(uri + "songs");
            
            return await stringAsync;
        }
    }
}