using Domain.Album;
using Domain.Artist;
using Domain.Library;
using Domain.Play;
using Domain.Playlist;
using Domain.PlaylistManage;
using Domain.SongManage;
using Domain.SongSearch;
using Domain.Users;
using RestT2_T3;

namespace Factory
{
    public class NetworkingFactory
    {

        public static IAlbumNetworking GetAlbumNetworking()
        {
            return new AlbumRestClient();
        }

        public static IArtistNetworking GetArtistNetworking()
        {
            return new ArtistRestClient();
        }

        public static IPlayNetworking GetPlayNetworking()
        {
            return new PlayRestClient();
        }

        public static IPlaylistNetworking GetPlaylistNetworking()
        {
            return new PlaylistRestClient();
        }

        public static IPlaylistMangeNetworking GetPlaylistMangeNetworking()
        {
            return new PlaylistManageRestClient();
        }

        public static ISongManageNetworking GetSongManageNetworking()
        {
            return new SongManageRestClient();
        }

        public static ILibraryNetworking GetLibraryNetworking()
        {
            return new LibraryRestClient();
        }

        public static ISongSearchNetworking GetSongSearchNetworking()
        {
            return new SongSearchRestClient();
        }

        public static IUserNetworking GetUserNetworking()
        {
            return new UserRestClient();
        }
        
        
    }
}