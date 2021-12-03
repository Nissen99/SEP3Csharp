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
        

        public async Task DeleteExistingPlayListAsync(Entities.Playlist playlist)
        {
            await playlistNetworking.RemovePlaylistAsync(playlist);
        }

        public async Task<IList<Entities.Playlist>> GetAllPlaylistsForUserAsync(User user)
        {
            return await playlistNetworking.GetAllPlaylistsForUserAsync(user);
        }


        public async Task CreateNewPlaylistAsync(Entities.Playlist playlist)
        {
            if (!InputValidator.CheckPlaylist(playlist))
            {
                throw new ArgumentException("Some property not found");
            }

            await playlistNetworking.CreateNewPlaylistAsync(playlist);
        }

        public async Task<IList<Song>> GetAllSongsFromPlaylistAsync(Entities.Playlist playlist)
        {
            return await playlistNetworking.GetAllSongsFromPlaylistAsync(playlist);
        }
    }
}