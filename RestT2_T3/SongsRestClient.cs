using System.Threading.Tasks;
using Domain.Songs;

namespace RestT2_T3
{
    public class SongsRestClient : ISongsNetworking
    {
        public Task<string> GetAllSongs()
        {
            throw new System.NotImplementedException();
        }
    }
}