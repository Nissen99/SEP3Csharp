using System.Threading.Tasks;
using SocketsT1_T2.Tier2.Commands;

namespace SocketsT1_T2.Tier2.Util
{
    public interface IRequestHandler
    {
        Task ExecuteCommand();
        ICommand GetCommand();
    }
}