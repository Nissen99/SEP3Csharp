using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Library;
using Domain.Play;
using Entities;
using Factory;
using RestT2_T3;
using SocketsT1_T2.Shared;
using SocketsT1_T2.Tier2.Util;


/*
 * Commando klasse. Den klasse styrer udpakningen af handlingen 'Get All Songs'.
 * Denne handling har ingen objekter den sender videre til sin receiver ILibraryService, den returnerer en respons.
 */

namespace SocketsT1_T2.Tier2.Commands
{
    [MyCommand]
    public class GetAllSongsCommand : ICommand
    {
       
        private ILibraryService libraryService;
        public string Action { get; }
        public TransferObj RequestObj { get; set; }

        public GetAllSongsCommand()
        {
            libraryService = ServicesFactory.GetLibraryService();
            Action = "GETSONGS";
        }

        public async Task<TransferObj> Execute()
        {
            try
            {
                IList<Song> result = await libraryService.GetAllSongsAsync();
                return await ServerResponse.PrepareTransferObjectWithValueAsync(result);
            }
            catch (Exception e)
            {
                return await ServerResponse.PrepareTransferObjectWithExceptionAsync( e);
            }
        }

        


    }
}
