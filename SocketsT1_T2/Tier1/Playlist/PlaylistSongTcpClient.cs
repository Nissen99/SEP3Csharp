using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SocketsT1_T2.Tier1.Playlist
{
    public class PlaylistSongTcpClient : TcpClientBase, IPlaylistSongNetworkClient
    {
        public async Task<IList<Entities.Song>> GetAllSongsFromPlaylistAsync(Entities.Playlist playlist)
        {
            using TcpClient client = GetTcpClient();
            await SendServerRequest("GETSONGSFROMPLAYLIST", playlist, client);
            return await ServerResponse<IList<Entities.Song>>(client, 100000);
        }

    }
}