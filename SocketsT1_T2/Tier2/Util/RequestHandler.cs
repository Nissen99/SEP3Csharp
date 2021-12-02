using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Entities;
using SocketsT1_T2.Tier2.Commands;

namespace SocketsT1_T2.Tier2.Util
{
    public class RequestHandler
    {
        private Dictionary<string, ICommand> commands = new()
        {
            {"GETSONGS", new GetAllSongsCommand()},
            {"PLAYSONG", new PlaySongCommand()},
            {"ADDNEWSONG", new AddNewSongCommand()},
            {"GETSONGSBYFILTER", new GetSongsByFilterCommand()},
            {"REGISTERUSER", new RegisterUserCommand()},
            {"REMOVESONG", new RemoveSongCommand()},
            {"SEARCHFORALBUMS", new SearchForAlbumsCommand()},
            {"SEARCHFORARTISTS", new SearchForArtistsCommand()},
            {"VALIDATEUSER", new ValidateUserCommand()},
            {"GETALLALBUMS", new GetAllAlbumsCommand()},
            {"GETALLARTISTS", new GetAllArtistsCommand()},
            {"GETPLAYLISTS", new GetPlaylistsCommand()},
            {"GETSONGSFROMPLAYLIST", new GetSongsFromPlaylistCommand()}
        };

        public string RequestAction { get; set; }
        public JsonElement RequestArg { get; set; }
        private NetworkStream stream;
        private ICommand activeCommand;

        public RequestHandler(NetworkStream stream)
        {
            this.stream = stream;
            
        }
        
        private async Task SendToClient<T>(T TObject)
        {
            TransferObj<T> transferObj = new TransferObj<T>
            {
                Action = "RETURN", Arg = TObject
            };
            string transferAsJson = JsonSerializer.Serialize(transferObj);
            byte[] toServer = Encoding.UTF8.GetBytes(transferAsJson);
            await stream.WriteAsync(toServer, 0, toServer.Length);
        }

        private async Task GetRequest()
        {
            byte[] dataFromServer = new byte[30000000];
            int bytesRead = await stream.ReadAsync(dataFromServer, 0, dataFromServer.Length);
            string readFromClient = Encoding.UTF8.GetString(dataFromServer, 0, bytesRead);
            TransferObj<JsonElement> transferObj = JsonSerializer.Deserialize<TransferObj<JsonElement>>(readFromClient,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });
            RequestAction = transferObj.Action;
            RequestArg = transferObj.Arg;
        }

        private void SetCommand(string id)
        {
            activeCommand = null;
            if (!commands.TryGetValue(id, out activeCommand))
                activeCommand = null;

        }

        public async Task<ICommand> GetCommand()
        {
            await GetRequest();
            SetCommand(RequestAction);
            return activeCommand;
        }

        public async Task HandleRequest()
        {
            await GetRequest();
            SetCommand(RequestAction);

        }


    }
}