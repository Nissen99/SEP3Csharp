using System;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Play;
using Domain.Playlist;
using Entities;
using Factory;
using RestT2_T3;
using SocketsT1_T2.Shared;
using SocketsT1_T2.Tier2.Util;

namespace SocketsT1_T2.Tier2.Commands
{
    public class CreateNewPlaylistCommand:ICommand
    {

        private IPlayListService playListService;
        private NetworkStream stream;
        private TransferObj requestObj;
        
        public TransferObj ResponseObj { get; private set; }

        public CreateNewPlaylistCommand(NetworkStream stream, TransferObj requestObj)
        {
            playListService = ServicesFactory.GetPlayListService();
            this.stream = stream;
            this.requestObj = requestObj;
        }

        public async Task Execute()
        {
            try
            {
                Playlist playlist = JsonElementConverter.ElementToObject<Playlist>(requestObj.Arg);
                await playListService.CreateNewPlaylistAsync(playlist);
                ResponseObj = await ServerResponse.PrepareTransferObjectNoValueAsync(stream);
            }
            catch (Exception e)
            {
                ResponseObj = await ServerResponse.SendExceptionToClientAsync(stream, e);
            }
          
        }
    }
}