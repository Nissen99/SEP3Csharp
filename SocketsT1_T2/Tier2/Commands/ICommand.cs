using System.Threading.Tasks;
using SocketsT1_T2.Shared;

namespace SocketsT1_T2.Tier2.Commands
{
    public interface ICommand
    {
        string Action { get; }
        TransferObj RequestObj { get; set; }
        Task<TransferObj> Execute();
    }
}
