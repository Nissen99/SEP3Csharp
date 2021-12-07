using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Playlist;
using Entities;
using Factory;
using RestT2_T3;
using SocketsT1_T2.Tier2.Util;

namespace SocketsT1_T2.Tier2.Commands
{
    public class GetPlaylistsCommand : ICommand
    {
        private IPlayListService playListService = ServicesFactory.GetPlayListService();
        public async Task Execute(NetworkStream stream, string argFromTransfer)
        {
            try
            {
                User user = JsonElementConverter.ElementToObject<User>(argFromTransfer);
                IList<Playlist> result = await playListService.GetAllPlaylistsForUserAsync(user);
                await ServerResponse.SendToClientWithValueAsync(stream, result);
            }
            catch (Exception e)
            {
                await ServerResponse.SendExceptionToClientAsync(stream, e);
            }

        }
    }
}