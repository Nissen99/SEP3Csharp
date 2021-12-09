using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Playlist;
using Entities;
using Factory;
using RestT2_T3;
using SocketsT1_T2.Shared;
using SocketsT1_T2.Tier2.Util;

namespace SocketsT1_T2.Tier2.Commands
{
    public class GetPlaylistsCommand : ICommand
    {
        private IPlayListService playListService;
        private TransferObj requestObj;
        

        public GetPlaylistsCommand(TransferObj requestObj)
        {
            playListService = ServicesFactory.GetPlayListService();
            this.requestObj = requestObj;
        }

        public async Task<TransferObj> Execute()
        {
            try
            {
                User user = JsonElementConverter.ElementToObject<User>(requestObj.Arg);
                IList<Playlist> result = await playListService.GetAllPlaylistsForUserAsync(user);
                return await ServerResponse.PrepareTransferObjectWithValueAsync(result);
            }
            catch (Exception e)
            {
                return await ServerResponse.SendExceptionToClientAsync(e);
            }

        }
    }
}