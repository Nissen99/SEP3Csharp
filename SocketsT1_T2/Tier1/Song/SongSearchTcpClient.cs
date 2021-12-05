using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SocketsT1_T2.Tier1.Song
{
    public class SongSearchTcpClient : TcpClientBase, ISongSearchNetworkClient
    {
        public async Task<IList<Entities.Song>> GetSongsByFilterAsync(string[] filterOptions)
        {
            using TcpClient client = GetTcpClient();
            await SendServerRequest("GETSONGSBYFILTER", filterOptions, client);
            return await ServerResponse<IList<Entities.Song>>(client, 500000);
        }


    }
}