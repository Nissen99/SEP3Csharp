using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.SongSearch;
using Entities;

namespace RestT2_T3
{
    public class SongSearchRestClient : HttpClientBase, ISongSearchNetworking
    {
        public async Task<IList<Song>> GetSongsByTitleAsync(string songTitle)
        {
            using HttpClient httpClient = new HttpClient();
            
            HttpResponseMessage responseMessage = await httpClient.GetAsync(Uri + $"songSearch/songTitle={songTitle}");

            return await HandleResponseGet<IList<Song>>(responseMessage);
        }

        public async Task<IList<Song>> GetSongsByArtistNameAsync(string artistName)
        {
            using HttpClient httpClient = new HttpClient();
            HttpResponseMessage responseMessage =
                await httpClient.GetAsync(Uri + $"songSearch/artistName={artistName}");

            return await HandleResponseGet<IList<Song>>(responseMessage);
        }

        public async Task<IList<Song>> GetSongsByAlbumTitleAsync(string albumTitle)
        {
            using HttpClient httpClient = new HttpClient();
            HttpResponseMessage responseMessage =
                await httpClient.GetAsync(Uri + $"songSearch/albumTitle={albumTitle}");

            return await HandleResponseGet<IList<Song>>(responseMessage);
        }

     

      
    }
}