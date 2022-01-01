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
 * Commando klasse. Den klasse styrer udpakningen af handlingen 'Get All Artists'.
 * Denne handling har ingen objekter den sender videre til sin receiver IArtistService, den returnerer en respons.
 */

namespace SocketsT1_T2.Tier2.Commands
{
    [MyCommand]
    public class GetAllArtistsCommand : ICommand
    {
        private IArtistService artistService;
        public string Action { get; }
        public TransferObj RequestObj { get; set; }
        

        public GetAllArtistsCommand()
        {
            Action = "GETALLARTISTS";
            artistService = ServicesFactory.GetArtistService();
        }

        public async Task<TransferObj> Execute()
        {
            try
            {
                IList<Artist> artists = await artistService.GetAllArtistsAsync();
                return await ServerResponse.PrepareTransferObjectWithValueAsync(artists);
            }
            catch (Exception e)
            {
                return await ServerResponse.PrepareTransferObjectWithExceptionAsync( e);
            }

        }
    }
}
