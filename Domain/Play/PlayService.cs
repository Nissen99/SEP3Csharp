using System.Collections.Generic;
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
        

        public async Task<byte[]> PlayAsync(Song song)
        {
            return await playNetworking.GetSongWithMP3(song);
        }
    }
}