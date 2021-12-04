using System.Threading.Tasks;

namespace SocketsT1_T2.Tier1.Playlist
{
    public interface IPlaylistManageNetworkClient
    {
        Task AddSongToPlaylistAsync(Entities.Playlist playlist, Entities.Song song);
        Task RemoveSongFromPlaylistAsync(Entities.Playlist playlist, Entities.Song song);
    }
}