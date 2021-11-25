using Domain.Library;
using Domain.Play;
using Domain.Playlist;
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
            IPlayNetworking playNetworking = new PlayRestClient();
            IPlayService playService = new PlayService(playNetworking);
            IPlaylistNetworking playlistNetworking = new PlaylistRestClient();
            IPlayListService playListService = new PlayListService(playlistNetworking);
            ISongSearchNetworking songSearchNetworking = new SongSearchRestClient();
            ISongSearchService songSearchService = new SongSearchService(songSearchNetworking);
            IUserNetworking userNetworking = new UserRestClient();
            IUserService userService = new UserService(userNetworking);

            IServer server = new Server(libraryService, playService, songSearchService, userService);
            server.startServer();
        }
    }
}