using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Blazor.Model
{
    public interface IPlayListModel
    {
        Task<PlayList> CreatePlaylist(PlayList playList, User user);
        Task<IList<PlayList>> GetAllPlayForUser(User user);
        Task RemoveSongFromPlaylist(PlayList playList, Song song);
        Task AddSongToPlaylist(PlayList playList, Song song);
        Task DeletePlayList(PlayList playList);
        



    }
}		
;
