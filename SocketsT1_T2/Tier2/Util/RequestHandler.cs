using System.Collections.Generic;
using System.Threading.Tasks;
using SocketsT1_T2.Shared;
using SocketsT1_T2.Tier2.Commands;

namespace SocketsT1_T2.Tier2.Util
{
    public class RequestHandler : IRequestHandler
    {
        private Dictionary<string, ICommand> commands;
        private ICommand activeCommand;
        private TransferObj requestObj;

        public RequestHandler(TransferObj tObj)
        {
            requestObj = tObj;
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