using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;
using SocketsT1_T2.Tier1;

namespace Blazor.Model.PlaylistModel
{
    public class PlayListModel:IPlayListModel
    {
        private IClient Client;


        public PlayListModel(IClient client)
        {
            Client = client;
        }


        public Task<Playlist> CreatePlaylistAsync(Playlist playlist, User user)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<Playlist>> GetAllPlaylistsForUserAsync(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveSongFromPlaylistAsync(Playlist playlist, Song song)
        {
            throw new System.NotImplementedException();
        }

        public Task AddSongToPlaylistAsync(Playlist playlist, Song song)
        {
            throw new System.NotImplementedException();
        }

        public Task DeletePlayListAsync(Playlist playlist)
        {
            throw new System.NotImplementedException();
        }
    }
}
