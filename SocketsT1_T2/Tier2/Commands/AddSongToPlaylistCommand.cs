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
 * Commando klasse. Den klasse styrer udpakningen af handlingen 'Add Song To Playlist'.
 * Den sender de udpakkede objekter til sin receiver IPlaylistManageService og returnerer en Server Response.
 */



namespace SocketsT1_T2.Tier2.Commands
{
    [MyCommand]
    public class AddSongToPlaylistCommand : ICommand
    {
        private IPlaylistManageService playlistManageService;
        public TransferObj RequestObj { get; set; }
        public string Action { get; }
        

        public AddSongToPlaylistCommand()
        {
            Action = "ADDSONGTOPLAYLIST";
            playlistManageService = ServicesFactory.GetPlaylistManageService();
        }

        public async Task<TransferObj> Execute()
        {
            try
            {
                JsonElement[] args = JsonElementConverter.ElementToObject<JsonElement[]>(RequestObj.Arg);
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
