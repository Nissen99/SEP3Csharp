using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.SongSearch;
using Entities;
using Factory;
using RestT2_T3;
using SocketsT1_T2.Tier2.Util;

namespace SocketsT1_T2.Tier2.Commands
{
    public class GetSongsByFilterCommand: ICommand
    {
        private ISongSearchService songSearchService = ServicesFactory.GetSongSearchService();
        public async Task Execute(NetworkStream stream, string argFromTransfer)
        {
            try
            {
                string[] toSearch = JsonElementConverter.ElementToObject<string[]>(argFromTransfer);
                IList<Song> songs = await songSearchService.GetSongsByFilterJsonAsync(toSearch);
                await ServerResponse.SendToClientWithValueAsync(stream, songs);
            }
            catch (Exception e)
            {
                await ServerResponse.SendExceptionToClientAsync(stream, e);
            }
           
        }
    }
}