using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Play;
using Entities;
using RestT2_T3;
using SocketsT1_T2.Tier2.Util;

namespace SocketsT1_T2.Tier2.Commands
{
    public class GetAllSongsCommand : ICommand
    {
        //TODO Factory pattern
        private IPlayService playService = new PlayService(new PlayRestClient());
        

        public async Task Execute(NetworkStream stream, string argFromTransfer)
        {
            try
            {
                //Parameter skal ikke bruges
                IList<Song> result = await playService.GetAllSongsAsync();
                await ServerResponse.SendToClientWithValueAsync(stream, result);
            }
            catch (Exception e)
            {
                await ServerResponse.SendExceptionToClientAsync(stream, e);
            }
           
            



        }

        


    }
}