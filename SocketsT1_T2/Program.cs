
using SocketsT1_T2.Tier2;

namespace SocketsT1_T2
{
    class Program
    {
        static void Main(string[] args)
        {
            IServer server = new Server();
            server.startServer();
        }
    }
}