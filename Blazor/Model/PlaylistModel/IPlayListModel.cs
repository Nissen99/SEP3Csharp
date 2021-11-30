using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Blazor.Model.PlaylistModel
{
    public interface IPlayListModel
    {
        Task<Playlist> CreatePlaylistAsync(Playlist playlist, User user);
        Task<IList<Playlist>> GetAllPlaylistsForUserAsync(User user);
        Task<IList<Song>> GetAllSongFromPlaylist(Playlist playlist);
        Task RemoveSongFromPlaylistAsync(Playlist playlist, Song song);
        Task AddSongToPlaylistAsync(Playlist playlist, Song song);
        Task DeletePlayListAsync(Playlist playlist);
        public Playlist CurrentPlaylist { get; set; }


    }
}		
;
