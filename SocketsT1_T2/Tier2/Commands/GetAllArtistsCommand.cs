using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Artist;
using Entities;
using RestT2_T3;
using SocketsT1_T2.Tier2.Util;


namespace SocketsT1_T2.Tier2.Commands
{
    public class GetAllArtistsCommand : ICommand
    {
        private IArtistService artistService = new ArtistService(new ArtistRestClient());
        public async Task Execute(NetworkStream stream, string argFromTransfer)
        {
            try
            {
                IList<Artist> artists = await artistService.GetAllArtistsAsync();
                await ServerResponse.SendToClientWithValueAsync(stream, artists);
            }
            catch (Exception e)
            {
                await ServerResponse.SendExceptionToClientAsync(stream, e);
            }

        }
    }
}