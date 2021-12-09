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
            
        private NetworkStream stream;
        private ICommand activeCommand;
        private TransferObj requestObj;

        public RequestHandler(NetworkStream stream, TransferObj requestObj)
        {
            this.stream = stream;
            this.requestObj = requestObj;
            commands = new()
            {
                {"GETSONGS", new GetAllSongsCommand()},
                {"PLAYSONG", new PlaySongCommand(requestObj)},
                {"UPLOADSONG", new UploadSongCommand(requestObj)},
                {"GETSONGSBYFILTER", new GetSongsByFilterCommand(requestObj)},
                {"REGISTERUSER", new RegisterUserCommand(requestObj)},
                {"REMOVESONG", new RemoveSongCommand(requestObj)},
                {"SEARCHFORALBUMS", new SearchForAlbumsCommand(requestObj)},
                {"SEARCHFORARTISTS", new SearchForArtistsCommand(requestObj)},
                {"VALIDATEUSER", new ValidateUserCommand(requestObj)},
                {"GETALLALBUMS", new GetAllAlbumsCommand()},
                {"GETALLARTISTS", new GetAllArtistsCommand()},
                {"GETPLAYLISTS", new GetPlaylistsCommand(requestObj)},
                {"GETPLAYLISTFROMID", new GetPlaylistFromId(requestObj)},
                {"CREATENEWPLAYLIST", new CreateNewPlaylistCommand(requestObj)},
                {"REMOVEPLAYLIST",new RemovePlaylistCommand(requestObj)},
                {"ADDSONGTOPLAYLIST", new AddSongToPlaylistCommand(requestObj)},
                {"REMOVESONGFROMPLAYLIST",new RemoveSongFromPlaylistCommand(requestObj)}
            };
            SetActiveCommand(requestObj.Action);
        }
        private void SetActiveCommand(string requestAction)
        {
            activeCommand = null;
            if (!commands.TryGetValue(requestAction, out activeCommand))
                activeCommand = new NullCommand();
        }

        public async Task<TransferObj> ExecuteCommand()
        {
           return await activeCommand.Execute();
        }

        public ICommand GetCommand()
        {
            return activeCommand;
        }
        

    }
}