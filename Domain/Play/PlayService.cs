using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Entities;

namespace Domain.Play
{
    public class PlayService : IPlayService
    {
        private IPlayNetworking playNetworking;

        public PlayService(IPlayNetworking playNetworking)
        {
            this.playNetworking = playNetworking;
        }

        public async Task<IList<Song>> GetAllSongsAsync()
        {
            return await playNetworking.GetAllSongs();
        }

        public async Task<byte[]> PlayAsync(Song song)
        {
            return await playNetworking.GetSongWithMP3(song);
        }
    }
}