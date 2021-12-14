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
    public class GetPlaylistFromId : ICommand
    {
        private IPlayListService playListService;
        private TransferObj requestObj;
        

        public GetPlaylistFromId( TransferObj requestObj)
        {
            playListService = ServicesFactory.GetPlayListService();
            this.requestObj = requestObj;
        }

        public async Task<TransferObj> Execute()
        {
            try
            {
                int playlistId = JsonElementConverter.ElementToObject<int>(requestObj.Arg);
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
