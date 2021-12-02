#nullable enable
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Entities;
using SocketsT1_T2.Tier2.Commands;


namespace SocketsT1_T2.Tier1
{
    public class Client : IClient
    {
        public async Task<IList<Song>> GetAllSongs()
        {
            using TcpClient client = GetTcpClient();
            await SendServerRequest("GETSONGS", "", client);
            return await serverResponse<IList<Song>>(client, 100000);
        }


        public async Task<Song> PlaySong(Song song)
        {
            using TcpClient client = GetTcpClient();
            await SendServerRequest("PLAYSONG", song, client);
            return await serverResponse<Song>(client, 30000000);
        }

        public async Task<IList<Song>> GetSongsByFilterAsync(string[] filterOptions)
        {
            using TcpClient client = GetTcpClient();
            await SendServerRequest("GETSONGSBYFILTER", filterOptions, client);
            return await serverResponse<IList<Song>>(client, 500000);
        }

        public async Task RegisterUser(User user)
        {
            using TcpClient client = GetTcpClient();
            await SendServerRequest("REGISTERUSER", user, client);
        }

        public async Task<User> validateUser(User user)
        {
            using TcpClient client = GetTcpClient();
            await SendServerRequest("VALIDATEUSER", user, client);
            return await serverResponse<User>(client, 5000);
        }

        public async Task<IList<Artist>> SearchForArtists(string name)
        {
            using TcpClient client = GetTcpClient();
            await SendServerRequest("SEARCHFORARTISTS", name, client);
            return await serverResponse<IList<Artist>>(client, 500000);
        }

        public async Task<IList<Album>> SearchForAlbums(string title)
        {
            using TcpClient client = GetTcpClient();
            await SendServerRequest("SEARCHFORALBUMS", title, client);
            return await serverResponse<IList<Album>>(client, 500000);
        }

        public async Task AddNewSongAsync(Song newSong)
        {
            using TcpClient client = GetTcpClient();
            await SendServerRequest("ADDNEWSONG", newSong, client);
        }

        public async Task RemoveSongAsync(Song song)
        {
            using TcpClient client = GetTcpClient();
            await SendServerRequest("REMOVESONG", song, client);
        }

        public async Task CreateNewPlaylistAsync(Playlist playlist)
        {
            using TcpClient client = GetTcpClient();
            await SendServerRequest("CREATENEWPLAYLIST", playlist, client);
        }

        public async Task<IList<Album>> GetAllAlbumsAsync()
        {
            using TcpClient client = GetTcpClient();
            await SendServerRequest("GETALLALBUMS", "", client);
            return await serverResponse<IList<Album>>(client, 500000);
        }

        public async Task<IList<Artist>> GetAllArtistAsync()
        {
            using TcpClient client = GetTcpClient();
            await SendServerRequest("GETALLARTISTS", "", client);
            return await serverResponse<IList<Artist>>(client, 500000);
        }

        public async Task RemovePlaylistAsync(Playlist playlist)
        {
            using TcpClient client = GetTcpClient();
            await SendServerRequest("REMOVEPLAYLIST", playlist, client);
        }

        public async Task<IList<Playlist>> GetAllPlaylistsForUserAsync(User user)
        {
            using TcpClient client = GetTcpClient();
            Console.WriteLine("På client, er vi her user: " + user.Username);
            await SendServerRequest("GETPLAYLISTS", user, client);

            return await serverResponse<IList<Playlist>>(client, 1000000);
        }

        public async Task<IList<Song>> GetAllSongsFromPlaylistAsync(Playlist playlist)
        {
            using TcpClient client = GetTcpClient();
            await SendServerRequest("GETSONGSFROMPLAYLIST", playlist, client);
            return await serverResponse<IList<Song>>(client, 100000);
        }

        public async Task AddSongToPlaylistAsync(Playlist playlist, Song song)
        {
            using TcpClient client = GetTcpClient();
            object[] toSent = {playlist, song};
            await SendServerRequest("ADDSONGTOPLAYLIST", toSent, client);
        }

        public async Task RemoveSongFromPlaylistAsync(Playlist playlist, Song song)
        {
            using TcpClient client = GetTcpClient();
            object[] toSent = {playlist, song};
            await SendServerRequest("REMOVESONGFROMPLAYLIST", toSent, client);
        }


        private async Task SendServerRequest<T>(string action, T TObject, TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            TransferObj<T> transferObj = new TransferObj<T>
            {
                Action = action, Arg = TObject
            };
            string transferAsJson = JsonSerializer.Serialize(transferObj);
            byte[] toServer = Encoding.UTF8.GetBytes(transferAsJson);
            await stream.WriteAsync(toServer);
        }


        private async Task<T> serverResponse<T>(TcpClient client, int bufferSize)
        {
            NetworkStream stream = client.GetStream();

            byte[] dataFromServer = new byte[bufferSize];
            int bytesRead = await stream.ReadAsync(dataFromServer, 0, dataFromServer.Length);

            string inFromServer = Encoding.UTF8.GetString(dataFromServer, 0, bytesRead);
            TransferObj<T> objectFromServer = JsonSerializer.Deserialize<TransferObj<T>>(inFromServer,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return objectFromServer.Arg;
        }


        private TcpClient GetTcpClient()
        {
            return new TcpClient("localhost", 1098);
        }
    }
}