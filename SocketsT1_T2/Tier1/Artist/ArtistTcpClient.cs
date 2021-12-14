using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
/*
 * Klassen står for håndtering af at få fat i artist information. Den extender TcpClientBase for at kunne benytte dens hjælpemetoder.
 */
namespace SocketsT1_T2.Tier1.Artist
{
    public class ArtistTcpClient : TcpClientBase, IArtistNetworkingClient
    {
        public async Task<IList<Entities.Artist>> GetAllArtistsAsync()
        {
            using TcpClient client = GetTcpClient();
            await SendServerRequest("GETALLARTISTS", "", client);
            return await ServerResponse<IList<Entities.Artist>>(client, 500000);
        }
        
        public async Task<IList<Entities.Artist>> SearchForArtists(string name)
        {
            using TcpClient client = GetTcpClient();
            await SendServerRequest("SEARCHFORARTISTS", name, client);
            return await ServerResponse<IList<Entities.Artist>>(client, 500000);
        }


    }
}