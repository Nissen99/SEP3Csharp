﻿using System.Threading.Tasks;
using Entities;
using SocketsT1_T2.Tier1;

namespace Blazor.Model.SongManagerModel
{
    public class SongManageModel : ISongManageModel
    {
        private IClient client;

        public SongManageModel(IClient client)
        {
            this.client = client;
        }

        public async Task AddNewSongAsync(Song newSong)
        {
            await client.AddNewSongAsync(newSong);
        }

        public async Task RemoveSongAsync(Song song)
        {
            await client.RemoveSongAsync(song);
        }
    }
}