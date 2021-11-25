using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Domain.Playlist
{
    public class PlayListService:IPlayListService

    {
        private IPlaylistNetworking playlistNetworking;
        public PlayListService(IPlaylistNetworking playlistNetworking)
        {
            this.playlistNetworking = playlistNetworking;
        }

        public Task<Entities.Playlist> CreatePlaylist(Entities.Playlist playlist)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<Entities.Playlist>> GetAllPlaylist()
        {
            throw new System.NotImplementedException();
        }

        public Task UpdatePlaylist(Entities.Playlist playlist)
        {
            throw new System.NotImplementedException();
        }

        public Task DeletePlayList(int playListID)
        {
            throw new System.NotImplementedException();
        }
    }
}