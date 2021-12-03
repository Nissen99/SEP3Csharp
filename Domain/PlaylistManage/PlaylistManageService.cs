using System;
using System.Linq;
using System.Threading.Tasks;
using Entities;

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