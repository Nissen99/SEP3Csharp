using System.Collections.Generic;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.SongSearch;
using Entities;
using RestT2_T3;
using SocketsT1_T2.Tier2.Util;

namespace SocketsT1_T2.Tier2.Commands
{
    public class GetSongsByFilterCommand: ICommand
    {
        private ISongSearchService songSearchService = new SongSearchService(new SongSearchRestClient());
        public async Task Execute(NetworkStream stream, JsonElement tObj)
        {
            string[] toSearch = JsonElementConverter.ElementToObject<string[]>(tObj);
            IList<Song> songs = await songSearchService.GetSongsByFilterJsonAsync(toSearch);
            await ServerResponse.SendToClient(stream, songs);
        }
    }
}