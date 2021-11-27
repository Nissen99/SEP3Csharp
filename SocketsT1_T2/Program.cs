using Domain.Album;
using Domain.Artist;
using Domain.Library;
using Domain.Play;
using Domain.Playlist;
using Domain.Songs;
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
            IArtistNetworking artistNetworking = new ArtistRestClient();
            IArtistService artistService = new ArtistService(artistNetworking);
            IAlbumNetworking albumNetworking = new AlbumRestClient();
            IAlbumService albumService = new AlbumService(albumNetworking);
            ISongManageNetworking songManageNetworking = new SongManageRestClient();
            ISongManageService songManageService = new SongManageService(songManageNetworking);

            IServer server = new Server(libraryService, playService, songSearchService, userService, artistService, albumService, songManageService);
            server.startServer();
        }
    }
}