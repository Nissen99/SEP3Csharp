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
        private TransferObj requestObj;
        

        public GetSongsByFilterCommand(TransferObj requestObj)
        {
            songSearchService = ServicesFactory.GetSongSearchService();
            this.requestObj = requestObj;
        }

        public async Task<TransferObj> Execute()
        {
            try
            {
                string[] toSearch = JsonElementConverter.ElementToObject<string[]>(requestObj.Arg);
                IList<Song> songs = await songSearchService.GetSongsByFilterJsonAsync(toSearch);
                return await ServerResponse.PrepareTransferObjectWithValueAsync(songs);
            }
            catch (Exception e)
            {
                return await ServerResponse.PrepareTransferObjectWithExceptionAsync(e);
            }
           
        }
    }
}