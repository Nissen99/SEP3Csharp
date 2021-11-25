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


        public AudioTestModel(IClient client)
        {
            this.client = client;
        }

        public async Task<IList<Song>> GetAllSongs()
        {
            return await client.GetAllSongs();
        }
        
    }
}