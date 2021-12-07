using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Entities;
using SocketsT1_T2.Shared;
using SocketsT1_T2.Tier2.Commands;

namespace SocketsT1_T2.Tier2.Util
{
    public class RequestHandler
    {
        private Dictionary<string, ICommand> commands;

        public string RequestAction { get; set; }
        public string RequestArg { get; set; }
        private NetworkStream stream;
        private ICommand activeCommand;
        private TransferObj requestObj;

        public RequestHandler(NetworkStream stream)
        {
            this.stream = stream;
            requestObj = Task.Run(async () => await GetRequest()).Result;
            commands = new()
            {
                {"GETSONGS", new GetAllSongsCommand(stream)},
                {"PLAYSONG", new PlaySongCommand(stream, requestObj)},
                {"UPLOADSONG", new UploadSongCommand(stream, requestObj)},
                {"GETSONGSBYFILTER", new GetSongsByFilterCommand(stream, requestObj)},
                {"REGISTERUSER", new RegisterUserCommand(stream,requestObj)},
                {"REMOVESONG", new RemoveSongCommand(stream,requestObj)},
                {"SEARCHFORALBUMS", new SearchForAlbumsCommand(stream,requestObj)},
                {"SEARCHFORARTISTS", new SearchForArtistsCommand(stream,requestObj)},
                {"VALIDATEUSER", new ValidateUserCommand(stream,requestObj)},
                {"GETALLALBUMS", new GetAllAlbumsCommand(stream)},
                {"GETALLARTISTS", new GetAllArtistsCommand(stream)},
                {"GETPLAYLISTS", new GetPlaylistsCommand(stream,requestObj)},
                {"GETPLAYLISTFROMID", new GetPlaylistFromId(stream,requestObj)},
                {"CREATENEWPLAYLIST", new CreateNewPlaylistCommand(stream,requestObj)},
                {"REMOVEPLAYLIST",new RemovePlaylistCommand(stream,requestObj)},
                {"ADDSONGTOPLAYLIST", new AddSongToPlaylistCommand(stream,requestObj)},
                {"REMOVESONGFROMPLAYLIST",new RemoveSongFromPlaylistCommand(stream,requestObj)}
            };
            SetCommand(requestObj.Action);
            
        }
        
        private async Task<TransferObj> GetRequest()
        {
            byte[] dataFromServer = new byte[30000000];
            int bytesRead = await stream.ReadAsync(dataFromServer, 0, dataFromServer.Length);
            string readFromClient = Encoding.UTF8.GetString(dataFromServer, 0, bytesRead);
            TransferObj transferObj = JsonSerializer.Deserialize<TransferObj>(readFromClient,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });
            // RequestAction = transferObj.Action;
            // RequestArg = transferObj.Arg;
            return transferObj;
        }

        private void SetCommand(string id)
        {
            activeCommand = null;
            if (!commands.TryGetValue(id, out activeCommand))
                activeCommand = null;

        }

        public async Task<ICommand> GetCommand()
        {
            return activeCommand;
        }

        public async Task HandleRequest()
        {
            await GetRequest();
            SetCommand(RequestAction);

        }


    }
}