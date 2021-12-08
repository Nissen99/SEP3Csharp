using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.SongSearch;
using Entities;
using Factory;
using RestT2_T3;
using SocketsT1_T2.Shared;
using SocketsT1_T2.Tier2.Util;

namespace SocketsT1_T2.Tier2.Commands
{
    public class GetSongsByFilterCommand: ICommand
    {
        private ISongSearchService songSearchService;
        private NetworkStream stream;
        private TransferObj requestObj;
        
        public TransferObj ResponseObj { get; private set; }

        public GetSongsByFilterCommand(NetworkStream stream, TransferObj requestObj)
        {
            songSearchService = ServicesFactory.GetSongSearchService();
            this.stream = stream;
            this.requestObj = requestObj;
        }

        public async Task Execute()
        {
            try
            {
                string[] toSearch = JsonElementConverter.ElementToObject<string[]>(requestObj.Arg);
                IList<Song> songs = await songSearchService.GetSongsByFilterJsonAsync(toSearch);
                ResponseObj = await ServerResponse.PrepareTransferObjectWithValueAsync(songs);
            }
            catch (Exception e)
            {
                ResponseObj =  await ServerResponse.SendExceptionToClientAsync(e);
            }
           
        }
    }
}