using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Playlist;
using Entities;

/*
 * Denne klasse står REST kommunikationen af Playlist relaterede requests
 */
namespace RestT2_T3
{
    public class PlaylistRestClient : HttpClientBase, IPlaylistNetworking
    {

        public async Task CreateNewPlaylistAsync(Playlist playlist)

        {
            using HttpClient client = new HttpClient();
            
            StringContent content = FromObjectToStringContentCamelCase(playlist);

            HttpResponseMessage responseMessage = await client.PostAsync(Uri + "playlist" , content);
            
            HandleResponseNoReturn(responseMessage);
            
        }
        
        public async Task<IList<Playlist>> GetAllPlaylistsForUserAsync(User user)
        {
            using HttpClient client = new HttpClient();
            
            HttpResponseMessage responseMessage = await client.GetAsync(Uri + $"playlist?username={user.Username}");

            return await HandleResponseGet<IList<Playlist>>(responseMessage);
        }
        
        public async Task<Playlist> GetPlaylistFromIdAsync(int playlistId)
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage responseMessage= await client.GetAsync(Uri + $"playlist?playlistId={playlistId}");
            return await HandleResponseGet<Playlist>(responseMessage);
 
        }
        

        public async Task RemovePlaylistAsync(Playlist playlist)
        {
            using HttpClient client = new HttpClient();

            HttpResponseMessage responseMessage = await client.DeleteAsync(Uri + $"/playlist/{playlist.Id}");
            
            HandleResponseNoReturn(responseMessage);
        }
    }
    }