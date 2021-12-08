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
            return new LibraryService(new LibraryRestClient());
        }

        public static IPlayService GetPlayService()
        {
            return new PlayService(new PlayRestClient());
        }

        public static IPlayListService GetPlayListService()
        {
            return new PlayListService(new PlaylistRestClient());
        }

        public static IPlaylistManageService GetPlaylistManageService()
        {
            return new PlaylistManageService(new PlaylistManageRestClient());
        }

        public static ISongManageService GetSongManageService()
        {
            return new SongManageService(new SongManageRestClient());
        }

        public static ISongSearchService GetSongSearchService()
        {
            return new SongSearchService(new SongSearchRestClient());
        }

        public static IUserService GetUserService()
        {
            return new UserService(new UserRestClient());
        }
        
        
    }
}