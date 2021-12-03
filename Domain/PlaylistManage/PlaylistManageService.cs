using System.Threading.Tasks;
using Entities;

namespace Domain.PlaylistManage
{
    public class PlaylistManageService : IPlaylistManageService
    {
        private IPlaylistMangeNetworking playlistMangeNetworking;
        public PlaylistManageService(IPlaylistMangeNetworking playlistMangeNetworking)
        {
            this.playlistMangeNetworking = playlistMangeNetworking;
        }

        public async Task AddSongToPlaylistAsync(Entities.Playlist playlist, Song song)
        {
            await playlistMangeNetworking.AddSongToPlaylistAsync(playlist, song);
        }

        public async Task RemoveSongFromPlaylistAsync(Entities.Playlist playlist, Song song)
        {
            await playlistMangeNetworking.RemoveSongFromPlaylistAsync(playlist, song);
        }
    }
}