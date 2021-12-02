using System.Threading.Tasks;
using Entities;
using SocketsT1_T2.Tier1;

namespace Blazor.Model.PlaylistManageModel
{
    public class PlaylistManageModel : IPlaylistManageModel
    {
        private IClient client;

        public PlaylistManageModel(IClient client)
        {
            this.client = client;
        }

        public async Task AddSongToPlaylist(Playlist playlist, Song song)
        {
            await client.AddSongToPlaylistAsync(playlist, song);
        }

        public async Task RemoveSongFromPlaylist(Playlist playlist, Song song)
        {
            await client.RemoveSongFromPlaylistAsync(playlist, song);
        }
    }
}