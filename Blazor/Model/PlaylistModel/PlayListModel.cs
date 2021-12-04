using System.Collections.Generic;
using System.Threading.Tasks;
using Blazor.Model.UserModel;
using Entities;
using SocketsT1_T2.Tier1;
using SocketsT1_T2.Tier1.Playlist;

namespace Blazor.Model.PlaylistModel
{
    public class PlayListModel : IPlayListModel
    {
        private IPlaylistNetworkClient playlistClient;
        private IPlaylistSongNetworkClient playlistSongClient;
        
        public Playlist CurrentPlaylist { get; set; }


        public PlayListModel(IPlaylistNetworkClient playlistClient, IPlaylistSongNetworkClient playlistSongClient)
        {
            this.playlistClient = playlistClient;
            this.playlistSongClient = playlistSongClient;
        }

        public async Task CreateNewPlatlistAsync(Playlist playlist)
        {
          await playlistClient.CreateNewPlaylistAsync(playlist);
        }

        public async Task<IList<Playlist>> GetAllPlaylistsForUserAsync(User user)
        {
            return await playlistClient.GetAllPlaylistsForUserAsync(user);
        }

        public async Task<IList<Song>> GetAllSongFromPlaylist(Playlist playlist)
        {
            return await playlistSongClient.GetAllSongsFromPlaylistAsync(playlist);
        }
        

        public async Task RemovePlayListAsync(Playlist playlist)

        {
           await playlistClient.RemovePlaylistAsync(playlist);
        }
        
    }
}
