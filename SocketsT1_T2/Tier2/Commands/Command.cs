using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using SocketsT1_T2.Shared;


namespace SocketsT1_T2.Tier2.Commands
{
    public interface ICommand
    {
        TransferObj ResponseObj { get; }
        Task Execute();
        
    }
}