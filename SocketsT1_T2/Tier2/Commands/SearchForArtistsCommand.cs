using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Artist;
using Entities;
using Factory;
using RestT2_T3;
using SocketsT1_T2.Tier2.Util;

namespace SocketsT1_T2.Tier2.Commands
{
    public class SearchForArtistsCommand: ICommand
    {
        private IArtistService artistService = ServicesFactory.GetArtistService();
        public async Task Execute(NetworkStream stream, string argFromTransfer)
        {
            try
            {
                string name = JsonElementConverter.ElementToObject<string>(argFromTransfer);
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