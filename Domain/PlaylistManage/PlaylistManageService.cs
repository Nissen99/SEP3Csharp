using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Util;
using Entities;

/*
 * Denne klasse står for håndtering af specifikke playlists. Heraf at tilføje til eller fjerne fra en playliste.
 */
namespace Domain.PlaylistManage
{
    public class PlaylistManageService : IPlaylistManageService
    {
        private IPlaylistMangeNetworking playlistMangeNetworking;
        public PlaylistManageService(IPlaylistMangeNetworking playlistMangeNetworking)
        {
            this.playlistMangeNetworking = playlistMangeNetworking;
        }

        public async Task AddSongToPlaylistAsync(Entities.Playlist playlist, Song song)
        {
            if (!InputValidator.CheckPlaylist(playlist) || !InputValidator.CheckSongValidWithoutMp3(song))
                throw new ArgumentException("No property found");
            if (playlist.Songs.FirstOrDefault(s =>s.Id == song.Id) != null)
            {
                throw new ArgumentException("That song is already in the playlist");
            }
            await playlistMangeNetworking.AddSongToPlaylistAsync(playlist, song);
        }

        public async Task RemoveSongFromPlaylistAsync(Entities.Playlist playlist, Song song)
        {
            await playlistMangeNetworking.RemoveSongFromPlaylistAsync(playlist, song);
        }
    }
}