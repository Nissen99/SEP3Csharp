using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.SongManage;
using Entities;
using RestT2_T3;
using SocketsT1_T2.Tier2.Util;

namespace SocketsT1_T2.Tier2.Commands
{
    public class UploadSongCommand : ICommand
    {
        private ISongManageService songManageService = new SongManageService(new SongManageRestClient());
        public async Task Execute(NetworkStream stream, string argFromTransfer)
        {
            JsonElement[] args = JsonElementConverter.ElementToObject<JsonElement[]>(argFromTransfer);
            Song toAdd = JsonElementConverter.ElementToObject<Song>(args[0].GetRawText());
            Mp3 mp3 = JsonElementConverter.ElementToObject<Mp3>(args[1].GetRawText());
            await songManageService.AddNewSongAsync(toAdd, mp3);
        }
    }
}