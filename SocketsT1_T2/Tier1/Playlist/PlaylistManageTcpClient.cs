using System.Net.Sockets;
using System.Threading.Tasks;

namespace SocketsT1_T2.Tier1.Playlist
{
    public class PlaylistManageTcpClient : TcpClientBase, IPlaylistManageNetworkClient
    {
        public async Task AddSongToPlaylistAsync(Entities.Playlist playlist, Entities.Song song)
        {
            using TcpClient client = GetTcpClient();
            object[] toSent = {playlist, song};
            await SendServerRequest("ADDSONGTOPLAYLIST", toSent, client);
            await ServerResponseCheckForException(client, 100000);
        }

        public async Task RemoveSongFromPlaylistAsync(Entities.Playlist playlist, Entities.Song song)
        {
            using TcpClient client = GetTcpClient();
            object[] toSent = {playlist, song};
            await SendServerRequest("REMOVESONGFROMPLAYLIST", toSent, client);
            await ServerResponseCheckForException(client, 100000);

        }

    }
}