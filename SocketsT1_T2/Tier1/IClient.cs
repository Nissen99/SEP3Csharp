using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace SocketsT1_T2.Tier1
{
    public interface IClient
    {
        Task<IList<Song>> GetAllSongs();
        Task<Song> PlaySong(Song song);

        
        Task<IList<Song>> GetSongsByFilterAsync(string[] filterOptions);
        
        Task RegisterUser(User user);
        Task<User> validateUser(User user);

        Task<IList<Artist>> SearchForArtists(string name);
        Task<IList<Album>> SearchForAlbums(string title);
        Task AddNewSongAsync(Song newSong);
        Task RemoveSongAsync(Song song);
        Task<IList<Playlist>> GetAllPlaylistsForUserAsync(User user);
        Task<IList<Song>> GetAllSongsFromPlaylistAsync(Entities.Playlist playlist);

        Task<IList<Album>> GetAllAlbumsAsync();
        Task<IList<Artist>> GetAllArtistAsync();
    }
}