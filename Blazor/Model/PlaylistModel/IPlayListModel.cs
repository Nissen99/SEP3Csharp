using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Blazor.Model.PlaylistModel
{
    public interface IPlayListModel
    {
        
        Task CreateNewPlatlistAsync(Playlist playlist);
        Task<IList<Playlist>> GetAllPlaylistsForUserAsync(User user);
        Task<IList<Song>> GetAllSongFromPlaylist(Playlist playlist);
        Task RemovePlayListAsync(Playlist playlist);
        public Playlist CurrentPlaylist { get; set; }


    }
}		
;
