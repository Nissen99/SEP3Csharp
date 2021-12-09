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

namespace SocketsT1_T2.Tier2.Commands
{
    public class GetAllSongsCommand : ICommand
    {
        //TODO Factory pattern
        private ILibraryService libraryService;

        public GetAllSongsCommand()
        {
            libraryService = ServicesFactory.GetLibraryService();
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
                return await ServerResponse.PrepareTransferObjectWithException( e);
            }
           
            



        }

        


    }
}