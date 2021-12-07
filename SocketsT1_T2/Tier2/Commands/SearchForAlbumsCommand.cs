using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Album;
using Entities;
using Factory;
using RestT2_T3;
using SocketsT1_T2.Shared;
using SocketsT1_T2.Tier2.Util;

namespace SocketsT1_T2.Tier2.Commands
{
    public class SearchForAlbumsCommand: ICommand
    {
        private IAlbumService albumService;
        private NetworkStream stream;
        private TransferObj requestObj;

        public SearchForAlbumsCommand(NetworkStream stream, TransferObj requestObj)
        {
            albumService = ServicesFactory.GetAlbumService();
            this.stream = stream;
            this.requestObj = requestObj;
        }

        public async Task Execute()
        {
            try
            {
                string title = JsonElementConverter.ElementToObject<string>(requestObj.Arg);
                IList<Album> artists = await albumService.SearchForAlbums(title);
                await ServerResponse.SendToClientWithValueAsync(stream, artists);
            }
            catch (Exception e)
            {
                await ServerResponse.SendExceptionToClientAsync(stream, e);

            }
            
        }
    }
}