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

/*
 * Commando klasse. Den klasse styrer udpakningen af handlingen at 'tilføje en sang til en playliste'.
 * Den sender de udpakkede objekter til sin receiver IPlaylistManageService.
 */


namespace SocketsT1_T2.Tier2.Commands
{
    public class AddSongToPlaylistCommand : ICommand
    {
        private IPlaylistManageService playlistManageService;
        private TransferObj requestObj;
        

        public AddSongToPlaylistCommand(TransferObj requestObj)
        {
            playlistManageService = ServicesFactory.GetPlaylistManageService();
            this.requestObj = requestObj;
        }

        public async Task<TransferObj> Execute()
        {
            try
            {
                JsonElement[] args = JsonElementConverter.ElementToObject<JsonElement[]>(requestObj.Arg);
                Playlist playlist = JsonElementConverter.ElementToObject<Playlist>(args[0].GetRawText());
                Song song = JsonElementConverter.ElementToObject<Song>(args[1].GetRawText());
                await playlistManageService.AddSongToPlaylistAsync(playlist, song);
                return await ServerResponse.PrepareTransferObjectNoValueAsync();
            }
            catch (Exception e)
            {
                return await ServerResponse.PrepareTransferObjectWithExceptionAsync(e);
            }
        }
    }
}
