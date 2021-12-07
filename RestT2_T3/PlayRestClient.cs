using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Play;
using Entities;
using RestT2_T3.Util;

namespace RestT2_T3
{
    public class PlayRestClient : HttpClientBase, IPlayNetworking
    {
        public async Task<byte[]> GetSongWithMP3(Song song)
        {
            using HttpClient client = new HttpClient();
            
            HttpResponseMessage responseMessage = await client.GetAsync(Uri + $"mp3?songPath={song.Mp3}");

            CheckForBadStatusCode(responseMessage);
            
            byte[] byteAsync = await responseMessage.Content.ReadAsByteArrayAsync();
            
            return byteAsync;
        }

        public async Task<IList<Song>> GetAllSongs()
        {
            using HttpClient client = new HttpClient();
            
            HttpResponseMessage responseMessage = await client.GetAsync(Uri + "song");

            return await HandleResponseGet<IList<Song>>(responseMessage);

        }
        
      
    }
}