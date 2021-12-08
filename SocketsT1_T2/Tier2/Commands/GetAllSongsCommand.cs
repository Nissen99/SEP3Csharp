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
        private NetworkStream stream;
        
        public TransferObj ResponseObj { get; private set; }
        public GetAllSongsCommand(NetworkStream stream)
        {
            this.stream = stream;
            libraryService = ServicesFactory.GetLibraryService();
        }

        public async Task Execute()
        {
            try
            {
                IList<Song> result = await libraryService.GetAllSongsAsync();
                ResponseObj =  await ServerResponse.PrepareTransferObjectWithValueAsync(stream, result);
            }
            catch (Exception e)
            {
                ResponseObj =  await ServerResponse.SendExceptionToClientAsync(stream, e);
            }
           
            



        }

        


    }
}