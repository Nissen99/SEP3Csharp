using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using Domain.Album;
using Entities;
using Factory;
using SocketsT1_T2.Shared;
using SocketsT1_T2.Tier2.Util;

namespace SocketsT1_T2.Tier2.Commands
{
    public class GetAllAlbumsCommand : ICommand
    {
        private IAlbumService albumService;
        private NetworkStream stream;
        
        public TransferObj ResponseObj { get; private set; }

        public GetAllAlbumsCommand(NetworkStream stream)
        {
            albumService = ServicesFactory.GetAlbumService();
            this.stream = stream;
        }

        public async Task Execute()
        {
            try
            {
                IList<Album> albums = await albumService.GetAllAlbumsAsync();
                ResponseObj = await ServerResponse.PrepareTransferObjectWithValueAsync(albums);
            }
            catch (Exception e)
            {
                ResponseObj = await ServerResponse.SendExceptionToClientAsync(e);
            }
           
        }
    }
}