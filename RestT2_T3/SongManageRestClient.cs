using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.SongManage;
using Entities;

namespace RestT2_T3
{
    public class SongManageRestClient : HttpClientBase, ISongManageNetworking
    {

        public async Task<Song> AddNewSongAsync(Song newSong)
        {
            using HttpClient httpClient = new HttpClient();
            

            StringContent newSongAsStringContent = FromObjectToStringContentCamelCase(newSong);

            HttpResponseMessage responseMessage = await httpClient.PostAsync(Uri + "/song", newSongAsStringContent);

            HandleResponseNoReturn(responseMessage);
            Uri uri = responseMessage.Headers.Location;

            HttpResponseMessage songResponseMessage = await httpClient.GetAsync(uri);

            return await HandleResponseGet<Song>(songResponseMessage);

        }

        public async Task RemoveSongAsync(Song songToRemove)
        {
            using HttpClient httpClient = new HttpClient();

            HttpResponseMessage responseMessage = await httpClient.DeleteAsync(Uri + $"/song/{songToRemove.Id}");
            
            HandleResponseNoReturn(responseMessage);

        }

        public async Task UploadMp3(Mp3 mp3)
        {
            using HttpClient httpClient = new HttpClient();
            StringContent content = FromObjectToStringContentCamelCase(mp3);
            HttpResponseMessage responseMessage = await httpClient.PostAsync(Uri + "/mp3", content);
            HandleResponseNoReturn(responseMessage);
        }
        
        
    }
    }