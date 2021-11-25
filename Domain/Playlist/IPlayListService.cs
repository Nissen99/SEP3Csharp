using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Domain.Playlist
{
    public interface IPlayListService
    {
        Task<Entities.Playlist> CreatePlaylist(Entities.Playlist playlist);
        Task<IList<Entities.Playlist>> GetAllPlaylist();
        Task UpdatePlaylist(Entities.Playlist playlist);
        Task DeletePlayList(int playListID);
        



    }
}		
;
