using System.Threading.Tasks;

namespace Domain.SongSearch
{
    public interface ISongSearchNetworking
    {
        Task<string> GetSongsByTitleAsync(string songTitle);
        Task<string> GetSongsByArtistNameAsync(string artistName);

        Task<string> GetSongsByAlbumTitleAsync(string albumTitle);
    }
}