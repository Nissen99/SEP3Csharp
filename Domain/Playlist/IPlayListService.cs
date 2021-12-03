using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Domain.Playlist
{
    public interface IPlayListService
    {
        Task CreateNewPlaylistAsync(Entities.Playlist playlist);
        Task<IList<Song>> GetAllSongsFromPlaylistAsync(Entities.Playlist playlist);
        Task RemovePlayListAsync(Entities.Playlist playlist);
        Task<IList<Entities.Playlist>> GetAllPlaylistsForUserAsync(User user);
    }
}
;