using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Domain.SongSearch
{
    public interface ISongSearchNetworking
    {
        Task<IList<Song>> GetSongsByTitleAsync(string songTitle);
        Task<IList<Song>> GetSongsByArtistNameAsync(string artistName);

        Task<IList<Song>> GetSongsByAlbumTitleAsync(string albumTitle);
    }
}