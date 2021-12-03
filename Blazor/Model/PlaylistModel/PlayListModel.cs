﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Blazor.Model.UserModel;
using Entities;
using SocketsT1_T2.Tier1;

namespace Blazor.Model.PlaylistModel
{
    public class PlayListModel:IPlayListModel
    {
        private IClient Client;
        
        public Playlist CurrentPlaylist { get; set; }


        public PlayListModel(IClient client)
        {
            Client = client;
        }

        public async Task CreateNewPlatListAsync(Playlist playlist)
        {
          await Client.CreateNewPlaylistAsync(playlist);
        }

        public async Task<IList<Playlist>> GetAllPlaylistsForUserAsync(User user)
        {
            return await Client.GetAllPlaylistsForUserAsync(user);
        }

        public async Task<IList<Song>> GetAllSongFromPlaylist(Playlist playlist)
        {
            return await Client.GetAllSongsFromPlaylistAsync(playlist);
        }
        

        public async Task RemovePlayListAsync(Playlist playlist)

        {
           await Client.RemovePlaylistAsync(playlist);
        }
        
    }
}
