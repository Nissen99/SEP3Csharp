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
    public class SearchForArtistsCommand: ICommand
    {
        private IArtistService artistService;
        private TransferObj requestObj;
        

        public SearchForArtistsCommand(TransferObj requestObj)
        {
            artistService = ServicesFactory.GetArtistService();
            this.requestObj = requestObj;
        }

        public async Task<TransferObj> Execute()
        {
            try
            {
                string name = JsonElementConverter.ElementToObject<string>(requestObj.Arg);
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
