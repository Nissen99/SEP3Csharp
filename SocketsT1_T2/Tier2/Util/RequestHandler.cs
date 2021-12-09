using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using SocketsT1_T2.Shared;
using SocketsT1_T2.Tier2.Commands;

namespace SocketsT1_T2.Tier2.Util
{
    public class RequestHandler : IRequestHandler
    {
        private Dictionary<string, ICommand> commands;
            
        private NetworkStream stream;
        private ICommand activeCommand;
        private TransferObj requestObj;

        public RequestHandler(NetworkStream networkStream, TransferObj tObj)
        {
            stream = networkStream;
            requestObj = tObj;
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
            SetActiveCommand(requestObj.Action);
        }
        private void SetActiveCommand(string requestAction)
        {
            activeCommand = null;
            if (!commands.TryGetValue(requestAction, out activeCommand))
                activeCommand = new NullCommand();
        }

        public async Task ExecuteCommand()
        {
           await activeCommand.Execute();
        }

        public ICommand GetCommand()
        {
            return activeCommand;
        }
        

    }
}