using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
/*
 * Klassen står for håndtering at få fat i artist information. Den extender TcpClientBase for at kunne benytte dens hjælpemetoder.
 */
namespace SocketsT1_T2.Tier1.Album
{
    public class AlbumTcpClient : TcpClientBase, IAlbumNetworkClient
    {
        public async Task<IList<Entities.Album>> SearchForAlbums(string title)
        {
            using TcpClient client = GetTcpClient();
            await SendServerRequest("SEARCHFORALBUMS", title, client);
            return await ServerResponse<IList<Entities.Album>>(client, 500000);
        }

        public async Task<IList<Entities.Album>> GetAllAlbumsAsync()
        {
            using TcpClient client = GetTcpClient();
            await SendServerRequest("GETALLALBUMS", "", client);
            return await ServerResponse<IList<Entities.Album>>(client, 500000);
        }

    }
}