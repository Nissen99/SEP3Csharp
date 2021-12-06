using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Playlist;
using Entities;
using RestT2_T3;
using SocketsT1_T2.Tier2.Util;

namespace SocketsT1_T2.Tier2.Commands
{
    public class GetPlaylistFromId : ICommand
    {
        private IPlayListService playListService = new PlayListService(new PlaylistRestClient());
        public async Task Execute(NetworkStream stream, string argFromTransfer)
        {
            try
            {
                int playlistId = JsonElementConverter.ElementToObject<int>(argFromTransfer);
                Playlist playlist = await playListService.GetPlaylistFromIdAsync(playlistId);
                await ServerResponse.SendToClientWithValueAsync(stream, playlist);
            }
            catch (Exception e)
            {
                await ServerResponse.SendExceptionToClientAsync(stream, e);
            }
            
        }
    }
}