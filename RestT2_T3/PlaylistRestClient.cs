using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Playlist;
using Entities;

namespace RestT2_T3
{
    public class PlaylistRestClient : HttpClientBase, IPlaylistNetworking
    {

        public async Task CreateNewPlaylistAsync(Playlist playlist)

        {
            using HttpClient client = new HttpClient();
            
            StringContent content = FromObjectToStringContentCamelCase(playlist);

            HttpResponseMessage responseMessage = await client.PostAsync(Uri + "playlist/" , content);
            
            HandleResponseNoReturn(responseMessage);
            
        }
        
        public async Task<IList<Playlist>> GetAllPlaylistsForUserAsync(User user)
        {
            using HttpClient client = new HttpClient();
            
            HttpResponseMessage responseMessage = await client.GetAsync(Uri + "playlist/" + user.Username);

            return await HandleResponseGet<IList<Playlist>>(responseMessage);
        }
        public async Task<IList<Song>> GetAllSongsFromPlaylistAsync(Playlist playlist)
        {
            using HttpClient client = new HttpClient();
           
            HttpResponseMessage responseMessage= await client.GetAsync(Uri + "playlistSongs/" + playlist.Id);

            return await HandleResponseGet<IList<Song>>(responseMessage);
 
        }
        

        public async Task RemovePlaylistAsync(Playlist playlist)
        {
            using HttpClient httpClient = new HttpClient();

            HttpResponseMessage responseMessage = await httpClient.DeleteAsync(Uri + $"/playlist/{playlist.Id}");
            
            HandleResponseNoReturn(responseMessage);
        }
    }
}