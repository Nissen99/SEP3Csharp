using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Util;
using Entities;

namespace Domain.Playlist
{
    public class PlayListService : IPlayListService


    {
        private IPlaylistNetworking playlistNetworking;

        public PlayListService(IPlaylistNetworking playlistNetworking)
        {
            this.playlistNetworking = playlistNetworking;
        }


        public async Task RemovePlayListAsync(Entities.Playlist playlist)
        {
            await playlistNetworking.RemovePlaylistAsync(playlist);
        }

        public async Task<IList<Entities.Playlist>> GetAllPlaylistsForUserAsync(User user)
        {
            return await playlistNetworking.GetAllPlaylistsForUserAsync(user);
        }

        public async Task RemoveSongFromPlaylistAsync(Entities.Playlist playlist, Song song)
        {
            await playlistNetworking.RemoveSongFromPlaylistAsync(playlist, song);
        }

        public async Task AddSongsToPlaylistAsync(Entities.Playlist playlist, IList<Song> songs)
        {
            await playlistNetworking.AddSongsToPlaylistAsync(playlist, songs);
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