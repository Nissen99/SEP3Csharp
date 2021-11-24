using System;
using System.Text.Json;
using System.Threading.Tasks;
using Entities;

namespace Domain.Play
{
    public class PlayService : IPlayService
    {
        private IDataEndPoint dataEndPoint = new DataEndPoint();

        public async Task<string> GetAllSongsAsJsonAsync()
        {
            
            string allSongs = await dataEndPoint.GetAllSongs();
            Console.WriteLine("Play: alls onsg: " + allSongs);
            TransferObj sentObj = new TransferObj() {Arg = allSongs};
            string transAsJson = JsonSerializer.Serialize(sentObj);
            
            
            return transAsJson;
        }

        public async Task<string> GetSongsByFilterJsonAsync(TransferObj tObj)
        {
            string songsToReturn = await dataEndPoint.GetSongsByFilter(tObj);
            TransferObj sentObj = new TransferObj() {Arg = songsToReturn};
            return JsonSerializer.Serialize(sentObj);
        }


        public async Task<string> PlayAsync(Song song)
        {
            return await dataEndPoint.GetSongWithMP3(song);
        }

    }
}