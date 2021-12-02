using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Library;
using Entities;

namespace RestT2_T3
{
    public class LibraryRestClient : HttpClientBase, ILibraryNetworking
    {
        public async Task PostAllSongs(IList<Song> songList)
        {
            using HttpClient client = new HttpClient();
            
            StringContent content = FromObjectToStringContentCamelCase(songList);

            HttpResponseMessage responseMessage = await client.PostAsync(Uri + "songs", content);
           
            HandleResponsePostAndRemove(responseMessage);

        }

        public async Task<IList<byte[]>> GetAllMP3()
        {
            using HttpClient client = new HttpClient();
            int count = 0;
            IList<byte[]> toReturn = new List<byte[]>();
            while (true)
            {
                try
                {
                    byte[] byteArrayAsync = await client.GetByteArrayAsync(Uri + "mp3/" + count++);
                    Console.WriteLine("Library size of mp3s: " + byteArrayAsync.Length);
                    toReturn.Add(byteArrayAsync);
                }
                catch (Exception e)
                {
                    break;
                }
            }

            return toReturn;
        }
    }
}