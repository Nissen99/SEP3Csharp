using System.Threading;
using System.Threading.Tasks;
using Blazor.Model;
using Blazor.Model.PlayModel;
using Blazor.Util.Playstate;
using Microsoft.AspNetCore.Components.Server.Circuits;

namespace Blazor.Util
{
    public class CircuitHandlerService : CircuitHandler
    {
        private IPlayModel _play;
        
        public CircuitHandlerService(IPlayModel play)
        {
            this._play = play;
        }
        
        public override async Task OnCircuitClosedAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            _play.Context.StopPlaying();
            
        }
        
    }
}