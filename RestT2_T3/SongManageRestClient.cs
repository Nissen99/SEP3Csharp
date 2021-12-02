﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Songs;
using Entities;

namespace RestT2_T3
{
    public class SongManageRestClient : HttpClientBase, ISongManageNetworking
    {

        public async Task AddNewSongAsync(Song newSong)
        {
            using HttpClient httpClient = new HttpClient();
            

            StringContent newSongAsStringContent = FromObjectToStringContentCamelCase(newSong);

            HttpResponseMessage responseMessage = await httpClient.PostAsync(Uri + "/song", newSongAsStringContent);
            
            HandleResponsePostAndRemove(responseMessage);
            
        }

        public async Task RemoveSongAsync(Song songToRemove)
        {
            using HttpClient httpClient = new HttpClient();

            HttpResponseMessage responseMessage = await httpClient.DeleteAsync(Uri + $"/song/{songToRemove.Id}");
            
            HandleResponsePostAndRemove(responseMessage);

        }
        
        
    }
    }