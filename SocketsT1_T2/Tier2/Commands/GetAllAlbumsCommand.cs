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
        
        public GetAllAlbumsCommand()
        {
            albumService = ServicesFactory.GetAlbumService();
        }

        public async Task<TransferObj> Execute()
        {
            try
            {
                IList<Album> albums = await albumService.GetAllAlbumsAsync();
                return await ServerResponse.PrepareTransferObjectWithValueAsync(albums);
            }
            catch (Exception e)
            {
                return await ServerResponse.PrepareTransferObjectWithExceptionAsync(e);
            }
           
        }
    }
}