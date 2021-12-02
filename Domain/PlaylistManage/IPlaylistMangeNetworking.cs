using System.Threading.Tasks;
using Entities;

namespace Domain.PlaylistManage
{
    public interface IPlaylistMangeNetworking
    {
        Task AddSongToPlaylistAsync(Entities.Playlist playlist, Song song);
        Task RemoveSongFromPlaylistAsync(Entities.Playlist playlist, Song song);
    }
}