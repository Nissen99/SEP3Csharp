using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Domain.Playlist
{
    public interface IPlaylistNetworking
    {
        Task<Entities.Playlist> CreatePlaylistAsync(Entities.Playlist playlist, User user);
        Task<IList<Entities.Playlist>> GetAllPlaylistsForUserAsync(User user);
        Task<IList<Song>> GetAllSongsFromPlaylistAsync(Entities.Playlist playlist);
        Task DeletePlayListAsync(Entities.Playlist playlist);
    }
}