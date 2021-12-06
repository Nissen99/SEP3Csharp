using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Blazor.Model.PlaylistModel
{
    public interface IPlayListModel
    {
        
        Task CreateNewPlaylistAsync(Playlist playlist);
        Task<IList<Playlist>> GetAllPlaylistsForUserAsync(User user);
        Task<Playlist> GetPlaylistFromIdAsync(int playlistId);    
        Task RemovePlayListAsync(Playlist playlist);


    }
}		
;
