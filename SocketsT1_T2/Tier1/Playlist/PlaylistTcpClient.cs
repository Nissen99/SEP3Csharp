using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SocketsT1_T2.Tier1.Playlist
{
    public class PlaylistTcpClient : TcpClientBase, IPlaylistNetworkClient
    {
        public async Task<IList<Entities.Playlist>> GetAllPlaylistsForUserAsync(Entities.User user)
        {
            using TcpClient client = GetTcpClient();
            await SendServerRequest("GETPLAYLISTS", user, client);

            return await serverResponse<IList<Entities.Playlist>>(client, 1000000);
        }
        
        public async Task CreateNewPlaylistAsync(Entities.Playlist playlist)
        {
            using TcpClient client = GetTcpClient();
            await SendServerRequest("CREATENEWPLAYLIST", playlist, client);
        }

        public async Task RemovePlaylistAsync(Entities.Playlist playlist)
        {
            using TcpClient client = GetTcpClient();
            await SendServerRequest("REMOVEPLAYLIST", playlist, client);
        }
    }
}