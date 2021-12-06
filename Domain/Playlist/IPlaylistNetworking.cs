using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Domain.Playlist
{
    public interface IPlaylistNetworking
    {
        public Task CreateNewPlaylistAsync(Entities.Playlist playlist);
        Task<IList<Entities.Playlist>> GetAllPlaylistsForUserAsync(User user);
        Task<Entities.Playlist> GetPlaylistFromIdAsync(int playlistId);
        Task RemovePlaylistAsync(Entities.Playlist playlist);
    }
}