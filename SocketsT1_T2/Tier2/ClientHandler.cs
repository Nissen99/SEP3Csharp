using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Album;
using Domain.Artist;
using Domain.Play;
using Domain.Songs;
using Domain.SongSearch;
using Domain.Users;
using Entities;
using SocketsT1_T2.Tier2.Commands;
using SocketsT1_T2.Tier2.Util;

namespace SocketsT1_T2.Tier2
{
    public class ClientHandler : IClientHandler
    {
        private TcpClient client;
        private IPlayService playSongService;
        private IUserService userService;
        private ISongSearchService songSearchService;
        private IArtistService artistService;
        private IAlbumService albumService;
        private ISongManageService songManageService;

        public ClientHandler(TcpClient client, IPlayService playSongService, IUserService userService, 
            ISongSearchService songSearchService, IArtistService artistService, IAlbumService albumService,
            ISongManageService songManageService)
        {
            this.client = client;
            this.playSongService = playSongService;
            this.userService = userService;
            this.songSearchService = songSearchService;
            this.artistService = artistService;
            this.albumService = albumService;
            this.songManageService = songManageService;
            

        }

        

        public async void ListenToClientAsync()
        {
            Console.WriteLine("LISTEN");

            RequestHandler rHandler = new RequestHandler(client.GetStream());
            ICommand command = await rHandler.GetCommand();
            Console.WriteLine("Action: " + rHandler.RequestAction);
            Console.WriteLine("Arg: " + rHandler.RequestArg);
            await command.Execute(rHandler.RequestArg, client.GetStream());


            //await SendToClient("RETURN", toServer);
            // switch (result.Action)
            // {
            //     case "GETSONGS":
            //         await GetAllSongsAsync();
            //         break;
            //     case "PLAYSONG":
            //         Song song = ElementToObject<Song>((JsonElement) result.Arg);
            //         await HandlePlaySongAsync(song, client.GetStream());
            //         break;
            //     case "GETSONGSBYFILTER":
            //         string[] getSongFilterOptions = ElementToObject<string[]>((JsonElement) result.Arg);
            //         await GetSongsByFilterAsync(getSongFilterOptions);
            //         break;
            //     case "REGISTERUSER":
            //         User registerUser = ElementToObject<User>((JsonElement) result.Arg);
            //         await RegisterUser(registerUser);
            //         break;
            //     case "VALIDATEUSER":
            //         User validateUser = ElementToObject<User>((JsonElement) result.Arg);
            //         await ValidateUser(validateUser);
            //         break;
            //     case "SEARCHFORARTISTS":
            //         string name = ElementToObject<string>((JsonElement) result.Arg);
            //         await SearchForArtists(name);
            //         break;
            //     case "SEARCHFORALBUMS":
            //         string title = ElementToObject<string>((JsonElement) result.Arg);
            //         await SearchForAlbum(title);
            //         break;
            //     case "ADDNEWSONG":
            //         Song newSong = ElementToObject<Song>((JsonElement) result.Arg);
            //         await AddNewSongAsync(newSong);
            //         break;
            //     case "REMOVESONG":
            //         Song songToRemove = ElementToObject<Song>((JsonElement) result.Arg);
            //         await RemoveSongAsync(songToRemove);
            //         break;
            // }

            client.Dispose();
        }

        // private async Task RemoveSongAsync(Song songToRemove)
        // {
        //     await songManageService.RemoveSongAsync(songToRemove);
        // }

        // private async Task AddNewSongAsync(Song newSong)
        // {
        //     await songManageService.AddNewSongAsync(newSong);
        // }

        // private async Task SearchForAlbum(string title)
        // {
        //     IList<Album> artists = await albumService.SearchForAlbums(title);
        //
        //     await SendToClient("RESPONSE FROM SERVER", artists);        }
        //
        // private async Task SearchForArtists(string name)
        // {
        //     IList<Artist> artists = await artistService.SearchForArtists(name);
        //
        //     await SendToClient("RESPONSE FROM SERVER", artists);
        //
        // }
        //
        // private T ElementToObject<T>(JsonElement element)
        // {
        //     string stringElement = element.GetRawText();
        //     return JsonSerializer.Deserialize<T>(stringElement,new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
        // }
        //
        // private async Task GetSongsByFilterAsync(string[] args)
        // {
        //     string transAsJson;
        //     try
        //     {
        //         IList<Song> songs = await songSearchService.GetSongsByFilterJsonAsync(args);
        //         TransferObj<IList<Song>> transferObj = new TransferObj<IList<Song>>() {Action = "Ok 200", Arg = songs};
        //         transAsJson = JsonSerializer.Serialize(transferObj);
        //     }
        //     catch (Exception e)
        //     {
        //         TransferObj<string> transferObj = new TransferObj<string>()
        //             {Action = "Bad Request 400", Arg = e.Message};
        //         transAsJson = JsonSerializer.Serialize(transferObj);
        //     }
        //
        //     byte[] bytes = Encoding.UTF8.GetBytes(transAsJson);
        //
        //     NetworkStream stream = client.GetStream();
        //     await stream.WriteAsync(bytes, 0, bytes.Length);
        // }
        //
        // private async Task<TransferObj<Object>> readFromClientAsync(NetworkStream stream)
        // {
        //     byte[] dataFromServer = new byte[8000000];
        //     int bytesRead = await stream.ReadAsync(dataFromServer, 0, dataFromServer.Length);
        //     string readFromClient = Encoding.UTF8.GetString(dataFromServer, 0, bytesRead);
        //     TransferObj<Object> transferObj = JsonSerializer.Deserialize<TransferObj<Object>>(readFromClient,
        //         new JsonSerializerOptions
        //         {
        //             PropertyNameCaseInsensitive = true,
        //         });
        //
        //     return transferObj;
        // }
        //
        //
        // public async Task GetAllSongsAsync()
        // {
        //     IList<Song> songs = await playSongService.GetAllSongsAsync();
        //
        //     await SendToClient("RESPONSE FROM SERVER", songs);
        // }
        //
        // private async Task RegisterUser(User user)
        // {
        //     await userService.RegisterUser(user);
        // }
        //
        // private async Task ValidateUser(User user)
        // {
        //     User toReturn = await userService.ValidateUser(user);
        //     await SendToClient("RETURN", toReturn);
        // }
        //
        //
        // private async Task SendToClient<T>(string action, T TObject)
        // {
        //     NetworkStream stream = client.GetStream();
        //     TransferObj<T> transferObj = new TransferObj<T>
        //     {
        //         Action = action, Arg = TObject
        //     };
        //     string transferAsJson = JsonSerializer.Serialize(transferObj);
        //     byte[] toServer = Encoding.UTF8.GetBytes(transferAsJson);
        //     await stream.WriteAsync(toServer, 0, toServer.Length);
        // }
        //
        //
        // private async Task HandlePlaySongAsync(Song song, NetworkStream stream)
        // {
        //     
        //     Song songWithMp3 = await playSongService.PlayAsync(song);
        //     
        //     
        //     TransferObj<Song> transferObj = new TransferObj<Song>() {Action = "Response", Arg = songWithMp3};
        //     
        //     string jsonTrans = JsonSerializer.Serialize(transferObj);
        //     
        //     
        //     byte[] bytes = Encoding.UTF8.GetBytes(jsonTrans);
        //     await stream.WriteAsync(bytes, 0, bytes.Length);
        // }
    }
}