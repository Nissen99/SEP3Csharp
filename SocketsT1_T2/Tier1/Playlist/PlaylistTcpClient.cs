using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
/*
 * Klassen står for håndtering af Playlist relaterede requests. Den extender TcpClientBase for at kunne benytte dens hjælpemetoder.
 */
namespace SocketsT1_T2.Tier1.Playlist
{
    public class PlaylistTcpClient : TcpClientBase, IPlaylistNetworkClient
    {
        public async Task<IList<Entities.Playlist>> GetAllPlaylistsForUserAsync(Entities.User user)
        {
            using TcpClient client = GetTcpClient();
            await SendServerRequest("GETPLAYLISTS", user, client);

            return await ServerResponse<IList<Entities.Playlist>>(client, 1000000);
        }
        
        public async Task<Entities.Playlist> GetPlaylistFromIdAsync(int playlistId)
        {
            using TcpClient client = GetTcpClient();
            await SendServerRequest("GETPLAYLISTFROMID", playlistId, client);
            return await ServerResponse<Entities.Playlist>(client, 100000);
        }
        
        public async Task CreateNewPlaylistAsync(Entities.Playlist playlist)
        {
            using TcpClient client = GetTcpClient();
            await SendServerRequest("CREATENEWPLAYLIST", playlist, client);
            await ServerResponseCheckForException(client, 100000);

        }

        public async Task RemovePlaylistAsync(Entities.Playlist playlist)
        {
            using TcpClient client = GetTcpClient();
            await SendServerRequest("REMOVEPLAYLIST", playlist, client);
            await ServerResponseCheckForException(client, 100000);

        }
    }
}