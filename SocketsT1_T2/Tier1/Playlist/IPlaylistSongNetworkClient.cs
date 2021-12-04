using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocketsT1_T2.Tier1.Playlist
{
    public interface IPlaylistSongNetworkClient
    {
        Task<IList<Entities.Song>> GetAllSongsFromPlaylistAsync(Entities.Playlist playlist);

    }
}