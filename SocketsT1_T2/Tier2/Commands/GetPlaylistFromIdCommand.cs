using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Playlist;
using Entities;
using Factory;
using RestT2_T3;
using SocketsT1_T2.Shared;
using SocketsT1_T2.Tier2.Util;

namespace SocketsT1_T2.Tier2.Commands
{
    public class GetPlaylistFromId : ICommand
    {
        private IPlayListService playListService;
        private NetworkStream stream;
        private TransferObj requestObj;

        public GetPlaylistFromId(NetworkStream stream, TransferObj requestObj)
        {
            playListService = ServicesFactory.GetPlayListService();
            this.stream = stream;
            this.requestObj = requestObj;
        }

        public async Task Execute()
        {
            try
            {
                int playlistId = JsonElementConverter.ElementToObject<int>(requestObj.Arg);
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