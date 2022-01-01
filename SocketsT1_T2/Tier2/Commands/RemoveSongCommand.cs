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
    [MyCommand]
    public class RemoveSongCommand: ICommand
    {
        private ISongManageService songManageService;
        public TransferObj RequestObj { get; set; }
        public string Action { get; }

        public RemoveSongCommand()
        {
            Action = "REMOVESONG";
            songManageService = ServicesFactory.GetSongManageService();
        }

        public async Task<TransferObj> Execute()
        {
            try
            {
                Song song = JsonElementConverter.ElementToObject<Song>(RequestObj.Arg);
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
