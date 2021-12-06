using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocketsT1_T2.Tier1.Playlist
{
    public interface IPlaylistNetworkClient
    {
        Task CreateNewPlaylistAsync(Entities.Playlist playlist);
        Task<IList<Entities.Playlist>> GetAllPlaylistsForUserAsync(Entities.User user);
        Task<Entities.Playlist> GetPlaylistFromIdAsync(int playlistId);

        Task RemovePlaylistAsync(Entities.Playlist playlist);

    }
}