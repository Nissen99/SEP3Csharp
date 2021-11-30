using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Domain.Playlist
{
    public interface IPlayListService
    {
        Task<Entities.Playlist> CreatePlaylistAsync(Entities.Playlist playlist, User user);
        Task<IList<Entities.Playlist>> GetAllPlaylistsForUserAsync(User user);
        Task RemoveSongFromPlaylistAsync(Entities.Playlist playlist, Song song);
        Task AddSongToPlaylistAsync(Entities.Playlist playlist, Song song);
        Task DeletePlayListAsync(Entities.Playlist playlist);
        Task<IList<Song>> GetAllSongsFromPlaylistAsync(Entities.Playlist playlist);
    }
}		
;
