﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Domain.Playlist
{
    public class PlayListService:IPlayListService

    {
        public Task<PlayList> CreatePlaylist(PlayList playList)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<PlayList>> GetAllPlaylist()
        {
            throw new System.NotImplementedException();
        }

        public Task UpdatePlaylist(PlayList playlist)
        {
            throw new System.NotImplementedException();
        }

        public Task DeletePlayList(int playListID)
        {
            throw new System.NotImplementedException();
        }
    }
}