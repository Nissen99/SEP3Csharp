using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Domain.Library
{
    public interface ILibraryNetworking
    {
        Task PostAllSongs(IList<Song> songList);
        Task<IList<byte[]>> GetAllMP3();
    }
}