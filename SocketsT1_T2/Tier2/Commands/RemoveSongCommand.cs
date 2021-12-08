using System;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.SongManage;
using Entities;
using Factory;
using RestT2_T3;
using SocketsT1_T2.Shared;
using SocketsT1_T2.Tier2.Util;

namespace SocketsT1_T2.Tier2.Commands
{
    public class RemoveSongCommand: ICommand
    {
        private ISongManageService songManageService;
        private NetworkStream stream;
        private TransferObj requestObj;
        
        public TransferObj ResponseObj { get; private set; }

        public RemoveSongCommand(NetworkStream stream, TransferObj requestObj)
        {
            songManageService = ServicesFactory.GetSongManageService();
            this.stream = stream;
            this.requestObj = requestObj;
        }

        public async Task Execute()
        {
            try
            {
                Song song = JsonElementConverter.ElementToObject<Song>(requestObj.Arg);
                await songManageService.RemoveSongAsync(song);
                ResponseObj = await ServerResponse.PrepareTransferObjectNoValueAsync(stream);
            }
            catch (Exception e)
            {
                ResponseObj = await ServerResponse.SendExceptionToClientAsync(stream, e);
            }
           
        }
    }
}