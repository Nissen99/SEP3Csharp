using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Blazor.Model.PlaylistModel
{
    public interface IPlayListModel
    {
        Task<Playlist> CreatePlaylistAsync(Playlist playlist, User user);
        Task<IList<Playlist>> GetAllPlaylistsForUserAsync(User user);
        Task RemoveSongFromPlaylistAsync(Playlist playlist, Song song);
        Task AddSongToPlaylistAsync(Playlist playlist, Song song);
        Task DeletePlayListAsync(Playlist playlist);
        



    }
}		
;
