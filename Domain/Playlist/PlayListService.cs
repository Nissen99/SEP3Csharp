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


        public Task<Entities.Playlist> CreatePlaylistAsync(Entities.Playlist playlist, User user)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<Entities.Playlist>> GetAllPlaylistsForUserAsync(User user)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveSongFromPlaylistAsync(Entities.Playlist playlist, Song song)
        {
            throw new System.NotImplementedException();
        }

        public Task AddSongToPlaylistAsync(Entities.Playlist playlist, Song song)
        {
            throw new System.NotImplementedException();
        }

        public Task DeletePlayListAsync(Entities.Playlist playlist)
        {
            throw new System.NotImplementedException();
        }
    }
}