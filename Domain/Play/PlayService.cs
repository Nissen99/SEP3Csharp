using System;
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

        public async Task<string> GetAllSongsAsJsonAsync()
        {
            
            string allSongs = await playNetworking.GetAllSongs();
            Console.WriteLine("Play: alls onsg: " + allSongs);
            TransferObj sentObj = new TransferObj() {Arg = allSongs};
            string transAsJson = JsonSerializer.Serialize(sentObj);
            
            
            return transAsJson;
        }
        public async Task<string> PlayAsync(Song song)
        {
            return await playNetworking.GetSongWithMP3(song);
        }

    }
}