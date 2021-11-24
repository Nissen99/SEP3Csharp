using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Library;
using Entities;

namespace RestT2_T3
{
    public class LibraryRestClient : ILibraryNetworking
    {
        public Task PostAllSongs(IList<Song> songList)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<byte[]>> GetAllMP3()
        {
            throw new System.NotImplementedException();
        }
    }
}