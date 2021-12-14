using System.Threading.Tasks;
using Entities;

/*
 * Den klasse står for håndteringen af at afspille en sang
 */
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