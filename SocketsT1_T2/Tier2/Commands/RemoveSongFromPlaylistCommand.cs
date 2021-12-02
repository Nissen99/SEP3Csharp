using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.PlaylistManage;
using Entities;
using RestT2_T3;
using SocketsT1_T2.Tier2.Util;

namespace SocketsT1_T2.Tier2.Commands
{
    public class RemoveSongFromPlaylistCommand : ICommand
    {
        private IPlaylistManageService playlistManageService =
            new PlaylistManageService(new PlaylistManageRestClient());
        public async Task Execute(NetworkStream stream, JsonElement tObj)
        {
            JsonElement[] args = JsonElementConverter.ElementToObject<JsonElement[]>(tObj);
            Playlist playlist = JsonElementConverter.ElementToObject<Playlist>(args[0]);
            Song song = JsonElementConverter.ElementToObject<Song>(args[1]);
            await playlistManageService.RemoveSongFromPlaylistAsync(playlist, song);
        }
    }
}