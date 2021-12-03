using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.SongManage;
using Entities;
using RestT2_T3;
using SocketsT1_T2.Tier2.Util;

namespace SocketsT1_T2.Tier2.Commands
{
    public class RemoveSongCommand: ICommand
    {
        private ISongManageService songManageService = new SongManageService(new SongManageRestClient());
        public async Task Execute(NetworkStream stream, string argFromTransfer)
        {
            Song song = JsonElementConverter.ElementToObject<Song>(argFromTransfer);
            await songManageService.RemoveSongAsync(song);
        }
    }
}