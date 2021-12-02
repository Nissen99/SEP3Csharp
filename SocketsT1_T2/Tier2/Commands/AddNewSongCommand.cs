using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.SongManage;
using Entities;
using RestT2_T3;
using SocketsT1_T2.Tier2.Util;

namespace SocketsT1_T2.Tier2.Commands
{
    public class AddNewSongCommand : ICommand
    {
        private ISongManageService songManageService = new SongManageService(new SongManageRestClient());
        public async Task Execute(NetworkStream stream, JsonElement tObj)
        {
            Song toAdd = JsonElementConverter.ElementToObject<Song>(tObj);
            await songManageService.AddNewSongAsync(toAdd);
        }
    }
}