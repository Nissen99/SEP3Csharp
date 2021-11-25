using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Blazor.Model
{
    public interface IPlayListModel
    {
        Task<Playlist> CreatePlaylist(Playlist playlist, User user);
        Task<IList<Playlist>> GetAllPlayForUser(User user);
        Task RemoveSongFromPlaylist(Playlist playlist, Song song);
        Task AddSongToPlaylist(Playlist playlist, Song song);
        Task DeletePlayList(Playlist playlist);
        



    }
}		
;
