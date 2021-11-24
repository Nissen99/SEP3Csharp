using System.Threading.Tasks;
using Domain.Play;
using Entities;

namespace RestT2_T3
{
    public class PlayRestClient : IPlayNetworking
    {
        public Task<string> GetSongWithMP3(Song song)
        {
            throw new System.NotImplementedException();
        }
    }
}