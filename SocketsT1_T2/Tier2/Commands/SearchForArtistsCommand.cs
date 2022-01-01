using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Artist;
using Entities;
using Factory;
using RestT2_T3;
using SocketsT1_T2.Shared;
using SocketsT1_T2.Tier2.Util;

/*
 * Commando klasse. Den klasse styrer udpakningen af handlingen 'Search For Artists'.
 * Den sender det udpakkede objekt til sin receiver IArtistService og returnerer en respons.
 */


namespace SocketsT1_T2.Tier2.Commands
{
    [MyCommand]
    public class SearchForArtistsCommand: ICommand
    {
        private IArtistService artistService;
        public TransferObj RequestObj { get; set; }
        public string Action { get; }

        public SearchForArtistsCommand()
        {
            Action = "SEARCHFORARTISTS";
            artistService = ServicesFactory.GetArtistService();
        }

        public async Task<TransferObj> Execute()
        {
            try
            {
                string name = JsonElementConverter.ElementToObject<string>(RequestObj.Arg);
                IList<Artist> artists = await artistService.SearchForArtists(name);
                return await ServerResponse.PrepareTransferObjectWithValueAsync(artists);
            }
            catch (Exception e)
            {
                return await ServerResponse.PrepareTransferObjectWithExceptionAsync(e);

            }
            
        }
    }
}
