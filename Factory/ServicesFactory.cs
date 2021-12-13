using Domain.Album;
using Domain.Artist;
using Domain.Library;
using Domain.Play;
using Domain.Playlist;
using Domain.PlaylistManage;
using Domain.SongManage;
using Domain.SongSearch;
using Domain.User;
using RestT2_T3;

namespace Factory
{
    public class ServicesFactory
    {
        public static IAlbumService GetAlbumService()
        {
            return new AlbumService(NetworkingFactory.GetAlbumNetworking());
        }

        public static IArtistService GetArtistService()
        {
            return new ArtistService(NetworkingFactory.GetArtistNetworking());
        }

        public static ILibraryService GetLibraryService()
        {
            return new LibraryService(NetworkingFactory.GetLibraryNetworking());
        }

        public static IPlayService GetPlayService()
        {
            return new PlayService(NetworkingFactory.GetPlayNetworking());
        }

        public static IPlayListService GetPlayListService()
        {
            return new PlayListService(NetworkingFactory.GetPlaylistNetworking());
        }

        public static IPlaylistManageService GetPlaylistManageService()
        {
            return new PlaylistManageService(NetworkingFactory.GetPlaylistMangeNetworking());
        }

        public static ISongManageService GetSongManageService()
        {
            return new SongManageService(NetworkingFactory.GetSongManageNetworking());
        }

        public static ISongSearchService GetSongSearchService()
        {
            return new SongSearchService(NetworkingFactory.GetSongSearchNetworking());
        }

        public static IUserService GetUserService()
        {
            return new UserService(NetworkingFactory.GetUserNetworking());
        }
        
        
    }
}