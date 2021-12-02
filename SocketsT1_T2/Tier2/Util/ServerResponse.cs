using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Entities;

namespace SocketsT1_T2.Tier2.Util
{
    public class ServerResponse
    {
        public static async Task SendToClient<T>(NetworkStream stream, T TObject)
        {
            TransferObj<T> transferObj = new TransferObj<T>
            {
                Action = "RETURN", Arg = TObject
            };
            string transferAsJson = JsonSerializer.Serialize(transferObj);
            byte[] toServer = Encoding.UTF8.GetBytes(transferAsJson);
            await stream.WriteAsync(toServer, 0, toServer.Length);
        }
    }
}