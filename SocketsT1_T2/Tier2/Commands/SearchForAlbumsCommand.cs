using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Album;
using Entities;
using Factory;
using RestT2_T3;
using SocketsT1_T2.Shared;
using SocketsT1_T2.Tier2.Util;

/*
 * Commando klasse. Den klasse styrer udpakningen af handlingen 'SearchForAlbums'.
 * Den sender det udpakkede objekt til sin receiver IAlbumService og returnerer en respons.
 */


namespace SocketsT1_T2.Tier2.Commands
{
    [MyCommand]
    public class SearchForAlbumsCommand: ICommand
    {
        private IAlbumService albumService;
        public TransferObj RequestObj { get; set; }
        public string Action { get; }

        public SearchForAlbumsCommand()
        {
            Action = "SEARCHFORALBUMS";
            albumService = ServicesFactory.GetAlbumService();
        }

        public async Task<TransferObj> Execute()
        {
            try
            {
                string title = JsonElementConverter.ElementToObject<string>(RequestObj.Arg);
                IList<Album> artists = await albumService.SearchForAlbums(title);
                return await ServerResponse.PrepareTransferObjectWithValueAsync( artists);
            }
            catch (Exception e)
            {
                return await ServerResponse.PrepareTransferObjectWithExceptionAsync(e);

            }
            
        }
    }
}
