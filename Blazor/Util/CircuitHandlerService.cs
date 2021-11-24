using System.Threading;
using System.Threading.Tasks;
using Blazor.Model;
using Microsoft.AspNetCore.Components.Server.Circuits;

namespace Blazor.Util
{
    public class CircuitHandlerService : CircuitHandler
    {
        private IPlayerModel player;
        
        public CircuitHandlerService(IPlayerModel player)
        {
            this.player = player;
        }
        
        public override async Task OnCircuitClosedAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            player.StopPlaying();
            
        }
        
    }
}