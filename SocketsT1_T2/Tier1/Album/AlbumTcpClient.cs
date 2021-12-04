using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SocketsT1_T2.Tier1.Album
{
    public class AlbumTcpClient : TcpClientBase, IAlbumNetworkClient
    {
        public async Task<IList<Entities.Album>> SearchForAlbums(string title)
        {
            using TcpClient client = GetTcpClient();
            await SendServerRequest("SEARCHFORALBUMS", title, client);
            return await serverResponse<IList<Entities.Album>>(client, 500000);
        }

        public async Task<IList<Entities.Album>> GetAllAlbumsAsync()
        {
            using TcpClient client = GetTcpClient();
            await SendServerRequest("GETALLALBUMS", "", client);
            return await serverResponse<IList<Entities.Album>>(client, 500000);
        }

    }
}