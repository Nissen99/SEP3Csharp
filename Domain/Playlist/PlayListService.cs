using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Util;
using Entities;
/*
 * Denne klasse står for håndtering af playlister. Heraf oprette, slette og at få fat i playlister.
 */
namespace Domain.Playlist
{
    public class PlayListService : IPlayListService


    {
        private IPlaylistNetworking playlistNetworking;

        public PlayListService(IPlaylistNetworking playlistNetworking)
        {
            this.playlistNetworking = playlistNetworking;
        }


        public async Task DeletePlayListAsync(Entities.Playlist playlist)
        {
            if (!InputValidator.CheckPlaylist(playlist)) throw new ArgumentException("No property found");
            await playlistNetworking.RemovePlaylistAsync(playlist);
        }

        public async Task<IList<Entities.Playlist>> GetAllPlaylistsForUserAsync(Entities.User user)
        {
            if (!InputValidator.ValidateUser(user)) throw new ArgumentException("No property found");
            return await playlistNetworking.GetAllPlaylistsForUserAsync(user);
        }

        public async Task<Entities.Playlist> GetPlaylistFromIdAsync(int playlistId)
        {
            if (!InputValidator.CheckPlaylistId(playlistId)) throw new ArgumentException("No property found");
            return await playlistNetworking.GetPlaylistFromIdAsync(playlistId);
        }


        public async Task CreateNewPlaylistAsync(Entities.Playlist playlist)
        {
            if (!InputValidator.CheckPlaylist(playlist)) throw new ArgumentException("Some property not found");
            
            await playlistNetworking.CreateNewPlaylistAsync(playlist);
        }
        
    }
}