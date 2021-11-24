using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Domain.Playlist
{
    public interface IPlayListService
    {
        Task<PlayList> CreatePlaylist(PlayList playList);
        Task<IList<PlayList>> GetAllPlaylist();
        Task UpdatePlaylist(PlayList playlist);
        Task DeletePlayList(int playListID);
        



    }
}		
;
