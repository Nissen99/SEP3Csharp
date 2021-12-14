using System.Threading.Tasks;
using Entities;
using SocketsT1_T2.Tier1;
using SocketsT1_T2.Tier1.Playlist;

/*
 * Klassen fungere gateway fra mvvm del til resten af systemet, her gennem kald til Client
 */
namespace Blazor.Model.PlaylistManageModel
{
    public class PlaylistManageModel : IPlaylistManageModel
    {
        private IPlaylistManageNetworkClient playlistManageClient;

        public PlaylistManageModel(IPlaylistManageNetworkClient playlistManageClient)
        {
            this.playlistManageClient = playlistManageClient;
        }

        public async Task AddSongToPlaylist(Playlist playlist, Song song)
        {
            await playlistManageClient.AddSongToPlaylistAsync(playlist, song);
        }

        public async Task RemoveSongFromPlaylist(Playlist playlist, Song song)
        {
            await playlistManageClient.RemoveSongFromPlaylistAsync(playlist, song);
        }
    }
}