using System.Collections.Generic;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Playlist;
using Entities;
using RestT2_T3;
using SocketsT1_T2.Tier2.Util;

namespace SocketsT1_T2.Tier2.Commands
{
    public class GetPlaylistsCommand : ICommand
    {
        private IPlayListService playListService = new PlayListService(new PlaylistRestClient());
        public async Task Execute(NetworkStream stream, JsonElement tObj)
        {
            User user = JsonElementConverter.ElementToObject<User>(tObj);
            IList<Playlist> result = await playListService.GetAllPlaylistsForUserAsync(user);
            await ServerResponse.SendToClient(stream, result);
        }
    }
}