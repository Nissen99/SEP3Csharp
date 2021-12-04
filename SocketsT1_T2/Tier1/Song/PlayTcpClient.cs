using System.Net.Sockets;
using System.Threading.Tasks;

namespace SocketsT1_T2.Tier1.Song
{
    public class PlayTcpClient : TcpClientBase, IPlayNetworkClient
    {
        public async Task<byte[]> PlaySong(Entities.Song song)
        {
            using TcpClient client = GetTcpClient();
            await SendServerRequest("PLAYSONG", song, client);
 
            return await serverResponse<byte[]>(client, 30000000);
        }


    }
}