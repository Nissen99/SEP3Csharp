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

/*
 * Commando klasse. Den klasse styrer udpakningen af handlingen 'Remove Song'.
 * Den sender det udpakkede objekt til sin receiver ISongManageService og returnerer en respons.
 */

namespace SocketsT1_T2.Tier2.Commands
{
    public class RemoveSongCommand: ICommand
    {
        private ISongManageService songManageService;
        private TransferObj requestObj;
        

        public RemoveSongCommand( TransferObj requestObj)
        {
            songManageService = ServicesFactory.GetSongManageService();
            this.requestObj = requestObj;
        }

        public async Task<TransferObj> Execute()
        {
            try
            {
                Song song = JsonElementConverter.ElementToObject<Song>(requestObj.Arg);
                await songManageService.RemoveSongAsync(song);
                return await ServerResponse.PrepareTransferObjectNoValueAsync();
            }
            catch (Exception e)
            {
                return await ServerResponse.PrepareTransferObjectWithExceptionAsync(e);
            }
           
        }
    }
}
