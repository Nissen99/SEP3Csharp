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

/*
 * Commando klasse. Den klasse styrer udpakningen af handlingen 'Play Song'.
 * Den sender det udpakkede objekt til sin receiver IPlayService og returnerer en respons.
 */

namespace SocketsT1_T2.Tier2.Commands
{
    public class PlaySongCommand : ICommand
    {
        private IPlayService playService;
        private TransferObj requestObj;
        


        public PlaySongCommand( TransferObj requestObj)
        {
            playService = ServicesFactory.GetPlayService();
            this.requestObj = requestObj;
        }

        public async Task<TransferObj> Execute()
        {
            try
            {
                Song tObjSong = JsonElementConverter.ElementToObject<Song>(requestObj.Arg);
                byte[] song = await playService.PlayAsync(tObjSong);
                return await ServerResponse.PrepareTransferObjectWithValueAsync(song);
            }
            catch (Exception e)
            {
                return await ServerResponse.PrepareTransferObjectWithExceptionAsync(e);
            }
          
        }
    }
}
