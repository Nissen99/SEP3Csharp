﻿using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Album;
using Entities;

namespace RestT2_T3
{
    public class AlbumRestClient : HttpClientBase, IAlbumNetworking
    {
        
        public async Task<IList<Album>> SearchForAlbums(string title)
        {
            using HttpClient client = new HttpClient();

            HttpResponseMessage responseMessage = await client.GetAsync(Uri + $"album/{title}");

            return await HandleResponseFromServer<IList<Album>>(responseMessage);
            
        }

        public async Task<IList<Album>> GetAllAlbumsAsync()
        {
            using HttpClient client = new HttpClient();
            
            HttpResponseMessage responseMessage = await client.GetAsync(Uri + $"album");

            return await HandleResponseFromServer<IList<Album>>(responseMessage);
          
        }
    }
}