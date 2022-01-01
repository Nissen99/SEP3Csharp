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

/*
 * Commando klasse. Den klasse styrer udpakningen af handlingen 'Remove Playlist'.
 * Den sender det udpakkede objekt til sin receiver IPlaylistService og returnerer en respons.
 */

namespace SocketsT1_T2.Tier2.Commands
{
    [MyCommand]
    public class RemovePlaylistCommand:ICommand

    {
        private IPlayListService playListService;
        public TransferObj RequestObj { get; set; }
        public string Action { get; }

        public RemovePlaylistCommand()
        {
            Action = "REMOVEPLAYLIST";
            playListService = ServicesFactory.GetPlayListService();
        }

        public async Task<TransferObj> Execute()
        {
            try
            {
                Playlist playlist = JsonElementConverter.ElementToObject<Playlist>(RequestObj.Arg);
                await playListService.DeletePlayListAsync(playlist);
                return await ServerResponse.PrepareTransferObjectNoValueAsync();
            }
            catch (Exception e)
            {
                return await ServerResponse.PrepareTransferObjectWithExceptionAsync(e);
            }
            

        }
    }
}
