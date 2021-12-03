using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.PlaylistManage;
using Entities;
using RestT2_T3;
using SocketsT1_T2.Tier2.Util;

namespace SocketsT1_T2.Tier2.Commands
{
    public class AddSongToPlaylistCommand : ICommand
    {
        private IPlaylistManageService playlistManageService =
            new PlaylistManageService(new PlaylistManageRestClient());
        public async Task Execute(NetworkStream stream, string argFromTransfer)
        {
            JsonElement[] args = JsonElementConverter.ElementToObject<JsonElement[]>(argFromTransfer);
            Playlist playlist = JsonElementConverter.ElementToObject<Playlist>(args[0].GetRawText());
            Song song = JsonElementConverter.ElementToObject<Song>(args[1].GetRawText());
            
            await playlistManageService.AddSongToPlaylistAsync(playlist, song);
            
        }
    }
}