using Domain.Album;
using Domain.Artist;
using Domain.Library;
using Domain.Play;
using Domain.Playlist;
using Domain.SongManage;
using Domain.SongSearch;
using Domain.Users;
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