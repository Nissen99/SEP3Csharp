using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using Domain.Album;
using Domain.Artist;
using Entities;
using Factory;
using RestT2_T3;
using SocketsT1_T2.Shared;
using SocketsT1_T2.Tier2.Util;

namespace SocketsT1_T2.Tier2.Commands
{
    public class GetAllAlbumsCommand : ICommand
    {
        private IAlbumService albumService = ServicesFactory.GetAlbumService();
        public async Task Execute(NetworkStream stream, string argFromTransfer)
        {
            try
            {
                IList<Album> albums = await albumService.GetAllAlbumsAsync();
                await ServerResponse.SendToClientWithValueAsync(stream,albums);
            }
            catch (Exception e)
            {
                await ServerResponse.SendExceptionToClientAsync(stream, e);
            }
           
        }
    }
}