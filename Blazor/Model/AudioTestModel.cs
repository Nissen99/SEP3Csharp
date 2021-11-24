using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Entities;
using SocketsT1_T2.Tier1;

namespace Blazor.Model
{
    public class AudioTestModel : IAudioTestModel
    {
        private IClient client;
        private IList<Song> songsToShow;
        
        
        public AudioTestModel(IClient client)
        {
            this.client = client;
        }

        public async Task<IList<Song>> GetAllSongs()
        {
            TransferObj transferObj = new TransferObj() {Action = "GETSONGS"};
            string transString = JsonSerializer.Serialize(transferObj);

            string inFromServer = await client.GetAllSongs(transString);
            Console.WriteLine("GET ALL SONGS trans : " + inFromServer);

            TransferObj tObj = JsonSerializer.Deserialize<TransferObj>(inFromServer,
                new JsonSerializerOptions() {PropertyNameCaseInsensitive = true});
            Console.WriteLine("Trans from server: "+ tObj.Action + "   " + tObj.Arg);
            IList<Song> allSongs = JsonSerializer.Deserialize<IList<Song>>(tObj.Arg,
                new JsonSerializerOptions() {PropertyNameCaseInsensitive = true});
            
            return allSongs;
        }
        
    }
}