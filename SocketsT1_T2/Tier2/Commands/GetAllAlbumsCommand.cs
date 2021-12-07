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
        private IAlbumService albumService;
        private NetworkStream stream;

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
                await ServerResponse.SendToClientWithValueAsync(stream,albums);
            }
            catch (Exception e)
            {
                await ServerResponse.SendExceptionToClientAsync(stream, e);
            }
           
        }
    }
}