using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using Domain.Playlist;
using Entities;
using Factory;
using SocketsT1_T2.Shared;
using SocketsT1_T2.Tier2.Util;

/*
 * Commando klasse. Den klasse styrer udpakningen af handlingen 'Get Playlist From Id'.
 * Den sender det udpakkede objekt til sin receiver IPlaylistService og returnerer en respons.
 */

namespace SocketsT1_T2.Tier2.Commands
{
    [MyCommand]
    public class GetPlaylistFromId : ICommand
    {
        private IPlayListService playListService;
        public TransferObj RequestObj { get; set; }
        public string Action { get; }
        

        public GetPlaylistFromId()
        {
            Action = "GETPLAYLISTFROMID";
            playListService = ServicesFactory.GetPlayListService();
        }

        public async Task<TransferObj> Execute()
        {
            try
            {
                int playlistId = JsonElementConverter.ElementToObject<int>(RequestObj.Arg);
                Playlist playlist = await playListService.GetPlaylistFromIdAsync(playlistId);
                return await ServerResponse.PrepareTransferObjectWithValueAsync(playlist);
            }
            catch (Exception e)
            {
                return await ServerResponse.PrepareTransferObjectWithExceptionAsync(e);
            }
        }
    }
}
