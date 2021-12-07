using System.Threading.Tasks;

namespace SocketsT1_T2.Tier2.Commands
{
    public class NullCommand : ICommand
    {
        public Task Execute()
        {
            throw new System.NotImplementedException("Den sendte request er ikke registreret commando");
        }
    }
}