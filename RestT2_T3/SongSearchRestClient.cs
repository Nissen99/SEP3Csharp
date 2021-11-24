using System.Threading.Tasks;
using Domain.SongSearch;

namespace RestT2_T3
{
    public class SongSearchRestClient : ISongSearchNetworking
    {
        public Task<string> GetSongsByTitleAsync(string songTitle)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetSongsByArtistNameAsync(string artistName)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetSongsByAlbumTitleAsync(string albumTitle)
        {
            throw new System.NotImplementedException();
        }
    }
}