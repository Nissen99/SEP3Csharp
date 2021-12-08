using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Domain.Playlist
{
    public interface IPlayListService
    {
        Task CreateNewPlaylistAsync(Entities.Playlist playlist);
        Task DeletePlayListAsync(Entities.Playlist playlist);
        Task<IList<Entities.Playlist>> GetAllPlaylistsForUserAsync(Entities.User user);
        
        Task<Entities.Playlist> GetPlaylistFromIdAsync(int playlistId);

    }
}
;