using Domain.Library;
using RestT2_T3;
using SocketsT1_T2.Tier2;

namespace SocketsT1_T2
{
    class Program
    {
        static void Main(string[] args)
        {
            ILibraryNetworking libraryNetworking = new LibraryRestClient();
            ILibraryService libraryService = new LibraryService(libraryNetworking);
            

            IServer server = new Server(libraryService);
            server.startServer();
        }
    }
}