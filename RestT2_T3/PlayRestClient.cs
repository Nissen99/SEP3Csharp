using System.Net.Http;
using System.Threading.Tasks;
using Domain.Play;
using Entities;
/*
 * Denne klasse står REST kommunikationen af afspilning af sang
 */
namespace RestT2_T3
{
    public class PlayRestClient : HttpClientBase, IPlayNetworking
    {
        public async Task<byte[]> GetSongWithMP3(Song song)
        {
            using HttpClient client = new HttpClient();
            
            HttpResponseMessage responseMessage = await client.GetAsync(Uri + $"mp3?songId={song.Id}");

            CheckForBadStatusCode(responseMessage);
            
            byte[] byteAsync = await responseMessage.Content.ReadAsByteArrayAsync();
            
            return byteAsync;
        }

    
      
    }
}