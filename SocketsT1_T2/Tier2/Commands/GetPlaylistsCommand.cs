using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using Domain.Playlist;
using Entities;
using Factory;
using SocketsT1_T2.Shared;
using SocketsT1_T2.Tier2.Util;

/*
 * Commando klasse. Den klasse styrer udpakningen af handlingen 'Get Playlists'.
 * Den sender det udpakkede objekt til sin receiver IPlaylistService og returnerer en respons.
 */

namespace SocketsT1_T2.Tier2.Commands
{
    [MyCommand]
    public class GetPlaylistsCommand : ICommand
    {
        private IPlayListService playListService;
        public TransferObj RequestObj { get; set; }
        public string Action { get; }
        

        public GetPlaylistsCommand()
        {
            Action = "GETPLAYLISTS";
            playListService = ServicesFactory.GetPlayListService();
        }

        public async Task<TransferObj> Execute()
        {
            try
            {
                User user = JsonElementConverter.ElementToObject<User>(RequestObj.Arg);
                IList<Playlist> result = await playListService.GetAllPlaylistsForUserAsync(user);
                return await ServerResponse.PrepareTransferObjectWithValueAsync(result);
            }
            catch (Exception e)
            {
                return await ServerResponse.PrepareTransferObjectWithExceptionAsync(e);
            }

        }
    }
}
