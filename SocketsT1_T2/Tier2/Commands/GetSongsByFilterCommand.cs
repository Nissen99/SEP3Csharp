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

/*
 * Commando klasse. Den klasse styrer udpakningen af handlingen 'Get Songs By Filter'.
 * Den sender det udpakkede objekt til sin receiver ISongSearchService og returnerer en respons.
 */

namespace SocketsT1_T2.Tier2.Commands
{
    [MyCommand]
    public class GetSongsByFilterCommand: ICommand
    {
        private ISongSearchService songSearchService;
        public TransferObj RequestObj { get; set; }
        public string Action { get; }
        

        public GetSongsByFilterCommand()
        {
            Action = "GETSONGSBYFILTER";
            songSearchService = ServicesFactory.GetSongSearchService();
        }

        public async Task<TransferObj> Execute()
        {
            try
            {
                string[] toSearch = JsonElementConverter.ElementToObject<string[]>(RequestObj.Arg);
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
