using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using SocketsT1_T2.Shared;
using SocketsT1_T2.Tier2.Commands;

/*
 * Request handler klassen står for håndtere requesten fra klienten. Den holder styr på hvilken kommando
 * systemet skal følge, og sender reqeusten ned til en Commando klasse til udpakning.
 */

namespace SocketsT1_T2.Tier2.Util
{
    public class RequestHandler : IRequestHandler
    {
        //private Dictionary<string, ICommand> commands;
        private ICommand activeCommand;

        public RequestHandler(TransferObj requestObj)
        {
            // commands = new()
            // {
            //     {"GETSONGS", new GetAllSongsCommand()},
            //     {"PLAYSONG", new PlaySongCommand(requestObj)},
            //     {"UPLOADSONG", new UploadSongCommand(requestObj)},
            //     {"GETSONGSBYFILTER", new GetSongsByFilterCommand(requestObj)},
            //     {"REGISTERUSER", new RegisterUserCommand(requestObj)},
            //     {"REMOVESONG", new RemoveSongCommand(requestObj)},
            //     {"SEARCHFORALBUMS", new SearchForAlbumsCommand(requestObj)},
            //     {"SEARCHFORARTISTS", new SearchForArtistsCommand(requestObj)},
            //     {"VALIDATEUSER", new ValidateUserCommand(requestObj)},
            //     {"GETALLALBUMS", new GetAllAlbumsCommand()},
            //     {"GETALLARTISTS", new GetAllArtistsCommand()},
            //     {"GETPLAYLISTS", new GetPlaylistsCommand(requestObj)},
            //     {"GETPLAYLISTFROMID", new GetPlaylistFromId(requestObj)},
            //     {"CREATENEWPLAYLIST", new CreateNewPlaylistCommand(requestObj)},
            //     {"REMOVEPLAYLIST",new RemovePlaylistCommand(requestObj)},
            //     {"ADDSONGTOPLAYLIST", new AddSongToPlaylistCommand(requestObj)},
            //     {"REMOVESONGFROMPLAYLIST",new RemoveSongFromPlaylistCommand(requestObj)}
            // };
            SetActiveCommand(requestObj.Action, requestObj);
        }

        private void SetActiveCommand(string requestAction, TransferObj requestObj)
        {
            IList<ICommand> list = CommandsReflection.Commands;
            activeCommand = list.First(c => c.Action == requestAction);
            activeCommand.RequestObj = requestObj;
            // activeCommand = null;
            // if (!commands.TryGetValue(requestAction, out activeCommand))
            //     activeCommand = new NullCommand();
        }

        public async Task<TransferObj> ExecuteCommand()
        {
           return await activeCommand.Execute();
        }
    }
}
