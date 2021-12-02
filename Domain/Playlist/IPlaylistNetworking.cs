using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Domain.Playlist
{
    public interface IPlaylistNetworking
    {
        public Task CreateNewPlaylistAsync(Entities.Playlist playlist);
        Task<IList<Entities.Playlist>> GetAllPlaylistsForUserAsync(User user);
        Task<IList<Song>> GetAllSongsFromPlaylistAsync(Entities.Playlist playlist);
        Task RemoveSongFromPlaylistAsync(Entities.Playlist playlist, Song song);
        Task AddSongsToPlaylistAsync(Entities.Playlist playlist, IList<Song> songs);
        Task RemovePlaylistAsync(Entities.Playlist playlist);
    }
}