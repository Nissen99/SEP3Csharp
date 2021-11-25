using System;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Play;
using Domain.SongSearch;
using Entities;

namespace SocketsT1_T2.Tier2
{
    public class ClientHandler : IClientHandler
    {
        private TcpClient client;
        private IPlayService playSongService;
        private ISongSearchService songSearchService;
        
        public ClientHandler(TcpClient client, IPlayService playSongService, ISongSearchService songSearchService)
        {
            this.client = client;
            this.playSongService = playSongService;
            this.songSearchService = songSearchService;

        }
        public async void ListenToClientAsync()
        {
            Console.WriteLine("LISTEN");
            TransferObj fromClient = readFromClientAsync(client.GetStream()).Result;

            switch (fromClient.Action)
            {
                case "GETSONGS":
                    await GetAllSongsAsync();
                    Console.WriteLine("SEND SONGS");
                    break;
                case "PLAYSONG":
                    Song song = JsonSerializer.Deserialize<Song>(fromClient.Arg);
                    await HandlePlaySongAsync(song, client.GetStream());
                    break;
                case "GETSONGSBYFILTER":
                    await GetSongsByFilterAsync(fromClient);
                    Console.WriteLine("SENDING FILTERED SONGS");
                    break;
            }

            client.Dispose();
        }

        private async Task GetSongsByFilterAsync(TransferObj tObj)
        {
            string transAsJson;
            try
            {
                string songsAsJSon = await songSearchService.GetSongsByFilterJsonAsync(tObj);
                TransferObj transferObj = new TransferObj(){Action = "Ok 200",Arg = songsAsJSon};
                transAsJson = JsonSerializer.Serialize(transferObj);

            }
            catch (Exception e)
            {
                TransferObj transferObj = new TransferObj() {Action = "Bad Request 400", Arg = e.Message};
                transAsJson = JsonSerializer.Serialize(transferObj);
            }
            
            byte[] bytes = Encoding.ASCII.GetBytes(transAsJson);

            NetworkStream stream = client.GetStream();
            await stream.WriteAsync(bytes, 0, bytes.Length);
            
        }

        private async Task<TransferObj> readFromClientAsync(NetworkStream stream)
        {
            byte[] dataFromServer = new byte[5000];
            int bytesRead = await stream.ReadAsync(dataFromServer, 0, dataFromServer.Length);
            string readFromClient = Encoding.ASCII.GetString(dataFromServer, 0, bytesRead);
            Console.WriteLine(readFromClient);
            TransferObj transferObj = JsonSerializer.Deserialize<TransferObj>(readFromClient);

            return transferObj;
        }


        public async Task GetAllSongsAsync()
        {
            string transAsJson = await playSongService.GetAllSongsAsJsonAsync();
            byte[] bytes = Encoding.ASCII.GetBytes(transAsJson);

            Console.WriteLine("GetallSongs sending: {0} bytes", bytes.Length);

            NetworkStream stream = client.GetStream();
            await stream.WriteAsync(bytes, 0, bytes.Length);
        }

        private async Task HandlePlaySongAsync(Song song, NetworkStream stream)
        {
            
            string jsonSong = await playSongService.PlayAsync(song);
            byte[] bytes = Encoding.ASCII.GetBytes(jsonSong);
            await stream.WriteAsync(bytes, 0, bytes.Length);
        }
    }
}