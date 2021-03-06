using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;
using SocketsT1_T2.Tier1.Playlist;

/*
 * Klassen fungere gateway fra mvvm del til resten af systemet, her gennem kald til Client
 */
namespace Blazor.Model.PlaylistModel
{
    public class PlayListModel : IPlayListModel
    {
        private IPlaylistNetworkClient playlistClient;
        

        public PlayListModel(IPlaylistNetworkClient playlistClient)
        {
            this.playlistClient = playlistClient;
        }

        public async Task CreateNewPlaylistAsync(Playlist playlist)
        {
          await playlistClient.CreateNewPlaylistAsync(playlist);
        }

        public async Task<IList<Playlist>> GetAllPlaylistsForUserAsync(User user)
        {
            return await playlistClient.GetAllPlaylistsForUserAsync(user);
        }

        public async Task<Playlist> GetPlaylistFromIdAsync(int playlistId)
        {
            return await playlistClient.GetPlaylistFromIdAsync(playlistId);
        }
        

        public async Task RemovePlayListAsync(Playlist playlist)

        {
           await playlistClient.RemovePlaylistAsync(playlist);
        }
        
    }
}
