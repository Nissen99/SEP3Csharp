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
 * 
 */
/*
 * Commando klasse. Den klasse styrer udpakningen af handlingen 'Upload Song'.
 * Den sender de udpakkede objekter til sin receiver ISongManageService og returnerer en respons.
 */


namespace SocketsT1_T2.Tier2.Commands
{
    [MyCommand]
    public class UploadSongCommand : ICommand
    {
        private ISongManageService songManageService;
        public TransferObj RequestObj { get; set; }
        public string Action { get; }

        public UploadSongCommand()
        {
            Action = "UPLOADSONG";
            songManageService = ServicesFactory.GetSongManageService();
        }

        public async Task<TransferObj> Execute()
        {
            try
            {
                JsonElement[] args = JsonElementConverter.ElementToObject<JsonElement[]>(RequestObj.Arg);
                Song toAdd = JsonElementConverter.ElementToObject<Song>(args[0].GetRawText());
                Mp3 mp3 = JsonElementConverter.ElementToObject<Mp3>(args[1].GetRawText());
                await songManageService.AddNewSongAsync(toAdd, mp3);
                return await ServerResponse.PrepareTransferObjectNoValueAsync();
            }
            catch (Exception e)
            {
                return await ServerResponse.PrepareTransferObjectWithExceptionAsync(e);
            }
            
        }
    }
}
