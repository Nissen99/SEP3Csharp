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
    public class GetAllArtistsCommand : ICommand
    {
        private IArtistService artistService;
        private NetworkStream stream;

        public GetAllArtistsCommand(NetworkStream stream)
        {
            artistService = ServicesFactory.GetArtistService();
            this.stream = stream;
        }

        public async Task Execute()
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