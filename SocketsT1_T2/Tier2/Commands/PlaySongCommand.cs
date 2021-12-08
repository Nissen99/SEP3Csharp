using System;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Play;
using Entities;
using Factory;
using RestT2_T3;
using SocketsT1_T2.Shared;
using SocketsT1_T2.Tier2.Util;

namespace SocketsT1_T2.Tier2.Commands
{
    public class PlaySongCommand : ICommand
    {
        private IPlayService playService;
        private NetworkStream stream;
        private TransferObj requestObj;
        private string tObjAsJson;
        

        public PlaySongCommand(NetworkStream stream, TransferObj requestObj)
        {
            this.stream = stream;
            playService = ServicesFactory.GetPlayService();
            this.requestObj = requestObj;
            //this.tObjAsJson = tObjAsJson;
        }

        public async Task Execute()
        {
            try
            {
                Song tObjSong = JsonElementConverter.ElementToObject<Song>(requestObj.Arg);
                byte[] song = await playService.PlayAsync(tObjSong);
                await ServerResponse.SendToClientWithValueAsync(stream, song);
            }
            catch (Exception e)
            {
                await ServerResponse.SendExceptionToClientAsync(stream, e);
            }
          
        }
    }
}