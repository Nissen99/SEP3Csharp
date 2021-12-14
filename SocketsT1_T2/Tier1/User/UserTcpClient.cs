using System.Net.Sockets;
using System.Threading.Tasks;

/*
 * Klassen står for håndtering af User relaterede requests. Den extender TcpClientBase for at kunne benytte dens hjælpemetoder.
 */
namespace SocketsT1_T2.Tier1.User
{
    public class UserTcpClient : TcpClientBase, IUserNetworkClient
    {
        public async Task RegisterUser(Entities.User user)
        {
            using TcpClient client = GetTcpClient();
            await SendServerRequest("REGISTERUSER", user, client);
            await ServerResponseCheckForException(client, 100000);

        }

        public async Task<Entities.User> ValidateUser(Entities.User user)
        {
            using TcpClient client = GetTcpClient();
            await SendServerRequest("VALIDATEUSER", user, client);
            return await ServerResponse<Entities.User>(client, 5000);
        }

    }
}