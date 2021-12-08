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

namespace SocketsT1_T2.Tier2.Commands
{
    public class SearchForArtistsCommand: ICommand
    {
        private IArtistService artistService;
        private NetworkStream stream;
        private TransferObj requestObj;

        public SearchForArtistsCommand(NetworkStream stream, TransferObj requestObj)
        {
            artistService = ServicesFactory.GetArtistService();
            this.stream = stream;
            this.requestObj = requestObj;
        }

        public async Task Execute()
        {
            try
            {
                string name = JsonElementConverter.ElementToObject<string>(requestObj.Arg);
                IList<Artist> artists = await artistService.SearchForArtists(name);
                await ServerResponse.SendToClientWithValueAsync(stream, artists);
            }
            catch (Exception e)
            {
                await ServerResponse.SendExceptionToClientAsync(stream, e);

            }
            
        }
    }
}