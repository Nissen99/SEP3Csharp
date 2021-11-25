using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;
using SocketsT1_T2.Tier1;

namespace Blazor.Model
{
    public class PlayListModel:IPlayListModel
    {
        private IClient Client;


        public PlayListModel(IClient client)
        {
            Client = client;
        }


        public Task<Playlist> CreatePlaylist(Playlist playlist, User user)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<Playlist>> GetAllPlayForUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveSongFromPlaylist(Playlist playlist, Song song)
        {
            throw new System.NotImplementedException();
        }

        public Task AddSongToPlaylist(Playlist playlist, Song song)
        {
            throw new System.NotImplementedException();
        }

        public Task DeletePlayList(Playlist playlist)
        {
            throw new System.NotImplementedException();
        }
    }
}
