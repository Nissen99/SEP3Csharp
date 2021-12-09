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
    public class AddSongToPlaylistCommand : ICommand
    {
        private IPlaylistManageService playlistManageService;
        private NetworkStream stream;
        private TransferObj requestObj;
        
        public TransferObj ResponseObj { get; private set; }

        public AddSongToPlaylistCommand(NetworkStream stream, TransferObj requestObj)
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
                await playlistManageService.AddSongToPlaylistAsync(playlist, song);
                ResponseObj = await ServerResponse.PrepareTransferObjectNoValueAsync();
            }
            catch (Exception e)
            {
                ResponseObj =  await ServerResponse.SendExceptionToClientAsync(e);
            }

        }
    }
}