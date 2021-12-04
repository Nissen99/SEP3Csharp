using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;
using SocketsT1_T2.Tier1;
using SocketsT1_T2.Tier1.Library;

namespace Blazor.Model.AudioTestModel
{
    public class AudioTestModel : IAudioTestModel
    {
        private ILibraryNetworkClient libraryNetworkClient;


        public AudioTestModel(ILibraryNetworkClient libraryNetworkClient)
        {
            this.libraryNetworkClient = libraryNetworkClient;
        }

        public async Task<IList<Song>> GetAllSongs()
        {
            return await libraryNetworkClient.GetAllSongs();
        }
        
    }
}