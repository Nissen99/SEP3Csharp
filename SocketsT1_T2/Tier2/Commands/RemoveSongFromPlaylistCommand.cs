using System;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.PlaylistManage;
using Entities;
using Factory;
using RestT2_T3;
using SocketsT1_T2.Shared;
using SocketsT1_T2.Tier2.Util;

namespace SocketsT1_T2.Tier2.Commands
{
    public class RemoveSongFromPlaylistCommand : ICommand
    {
        private IPlaylistManageService playlistManageService;
        private NetworkStream stream;
        private TransferObj requestObj;

        public RemoveSongFromPlaylistCommand(NetworkStream stream, TransferObj requestObj)
        {
            playlistManageService = ServicesFactory.GetPlaylistManageService();
            this.stream = stream;
            this.requestObj = requestObj;
        }

        public async Task Execute()
        {
            try
            {
                JsonElement[] args = JsonElementConverter.ElementToObject<JsonElement[]>(requestObj.Arg);
                Playlist playlist = JsonElementConverter.ElementToObject<Playlist>(args[0].GetRawText());
                Song song = JsonElementConverter.ElementToObject<Song>(args[1].GetRawText());
                await playlistManageService.RemoveSongFromPlaylistAsync(playlist, song);
                await ServerResponse.SendToClientNoValueAsync(stream);
            }
            catch (Exception e)
            {
                await ServerResponse.SendExceptionToClientAsync(stream, e);

            }
            
        }
    }
}