using System.Threading.Tasks;
using SocketsT1_T2.Shared;

namespace SocketsT1_T2.Tier2.Commands
{
    public class NullCommand : ICommand
    
    {
        public TransferObj ResponseObj { get; private set; }
        public Task Execute()
        {
            throw new System.NotImplementedException("Den sendte request er ikke registreret commando");
        }
    }
}