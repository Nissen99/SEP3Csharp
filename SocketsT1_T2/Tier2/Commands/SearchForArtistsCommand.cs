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
    public class SearchForArtistsCommand: ICommand
    {
        private IArtistService artistService = new ArtistService(new ArtistRestClient());
        public async Task Execute(NetworkStream stream, JsonElement tObj)
        {
            string name = JsonElementConverter.ElementToObject<string>(tObj);
            IList<Artist> artists = await artistService.SearchForArtists(name);
            await ServerResponse.SendToClient(stream, artists);
        }
    }
}