using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;


namespace SocketsT1_T2.Tier2.Commands
{
    public interface ICommand
    {
        Task Execute();
        //Task Execute(NetworkStream stream, string tObj);
    }
}