using System.Threading.Tasks;
using Entities;
using SocketsT1_T2.Tier1;

namespace Blazor.Model
{
    public class SongManageModel : ISongManageModel
    {
        private IClient client;

        public SongManageModel(IClient client)
        {
            this.client = client;
        }

        public async Task AddNewSongAsync(Song newSong)
        {
            await client.AddNewSongAsync(newSong);
        }
    }
}