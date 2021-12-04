using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace SocketsT1_T2.Tier1.Library
{
    public interface ILibraryNetworkClient
    {
        Task AddNewSongAsync(Entities.Song newSong, Mp3 mp3);
        Task RemoveSongAsync(Entities.Song song);
        Task<IList<Entities.Song>> GetAllSongs();

    }
}