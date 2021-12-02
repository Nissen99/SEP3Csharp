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
        public async Task<Song> GetSongWithMP3(Song song)
        {
            using HttpClient client = new HttpClient();
            // string stringAsync = await client.GetStringAsync(uri + $"songs/{song.Id}");
            //
            // Song songWithMP3 = JsonSerializer.Deserialize<Song>(stringAsync, new JsonSerializerOptions()
            // {
            //     PropertyNameCaseInsensitive = true,
            //     Converters = { new ByteArrayConverter() }
            // });
            //
            // return  songWithMP3;
            byte[] byteAsync = await client.GetByteArrayAsync(Uri + $"songs/{song.Id}");
            
            song.Mp3 = byteAsync;

            return song;

        }

        public async Task<IList<Song>> GetAllSongs()
        {
            using HttpClient client = new HttpClient();
            
            HttpResponseMessage responseMessage = await client.GetAsync(Uri + "songs");

            return await HandleResponseGet<IList<Song>>(responseMessage);

        }
        
      
    }
}