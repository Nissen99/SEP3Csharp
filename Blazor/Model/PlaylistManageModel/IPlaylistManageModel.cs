using System.Threading.Tasks;
using Entities;

namespace Blazor.Model.PlaylistManageModel
{
    public interface IPlaylistManageModel
    {
        Task AddSongToPlaylist(Playlist playlist, Song song);
        Task RemoveSongFromPlaylist(Playlist playlist, Song song);

    }
}