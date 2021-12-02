using System.Collections.Generic;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Album;
using Entities;
using RestT2_T3;
using SocketsT1_T2.Tier2.Util;

namespace SocketsT1_T2.Tier2.Commands
{
    public class GetAllAlbumsCommand : ICommand
    {
        private IAlbumService albumService = new AlbumService(new AlbumRestClient());
        public async Task Execute(NetworkStream stream, JsonElement tObj)
        {
            IList<Album> albums = await albumService.GetAllAlbumsAsync();
            await ServerResponse.SendToClient(stream,albums);
        }
    }
}