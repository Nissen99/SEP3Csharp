using System.Net.Sockets;
using System.Threading.Tasks;
using NAudio.Wave;

/*
 * Klassen står for håndtering af afspilning af en sang. Den extender TcpClientBase for at kunne benytte dens hjælpemetoder.
 */
namespace SocketsT1_T2.Tier1.Song
{
    public class PlayTcpClient : TcpClientBase, IPlayNetworkClient
    {
        public async Task<byte[]> PlaySong(Entities.Song song)
        {
            using TcpClient client = GetTcpClient();
            await SendServerRequest("PLAYSONG", song, client);
            return await ServerResponse<byte[]>(client, 30000000);
        }


    }
}