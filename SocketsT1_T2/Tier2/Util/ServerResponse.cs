using System;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Entities;
using SocketsT1_T2.Shared;

namespace SocketsT1_T2.Tier2.Util
{
    public class ServerResponse
    {
        public static async Task SendToClient<T>(NetworkStream stream, T TObject)
        {
            string objectAsJson = JsonSerializer.Serialize(TObject);
            string action = "RETURN";
            
            if (TObject is Exception)
            {
                 action = "Exeption";
            }
            TransferObj transferObj = new TransferObj
            {
                Action = action, Arg = objectAsJson
            };
            string transferAsJson = JsonSerializer.Serialize(transferObj);
            byte[] toServer = Encoding.UTF8.GetBytes(transferAsJson);
            await stream.WriteAsync(toServer, 0, toServer.Length);
        }
    }
}