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
    public class UploadSongCommand : ICommand
    {
        private ISongManageService songManageService;
        private NetworkStream stream;
        private TransferObj requestObj;
        
        public TransferObj ResponseObj { get; private set; }

        public UploadSongCommand(NetworkStream stream, TransferObj requestObj)
        {
            songManageService = ServicesFactory.GetSongManageService();
            this.stream = stream;
            this.requestObj = requestObj;

        }

        public async Task Execute()
        {
            try
            {
                JsonElement[] args = JsonElementConverter.ElementToObject<JsonElement[]>(requestObj.Arg);
                Song toAdd = JsonElementConverter.ElementToObject<Song>(args[0].GetRawText());
                Mp3 mp3 = JsonElementConverter.ElementToObject<Mp3>(args[1].GetRawText());
                await songManageService.AddNewSongAsync(toAdd, mp3);
                ResponseObj = await ServerResponse.PrepareTransferObjectNoValueAsync();
            }
            catch (Exception e)
            {
                ResponseObj = await ServerResponse.SendExceptionToClientAsync(e);
            }
            
        }
    }
}