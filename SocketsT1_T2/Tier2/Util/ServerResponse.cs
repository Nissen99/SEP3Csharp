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
        public static async Task SendToClientWithValueAsync<T>(NetworkStream stream, T TObject)
        {
            string objectAsJson = JsonSerializer.Serialize(TObject);
            string action = "RETURN";
            
            if (TObject is Error)
            {
                 action = "Exception";
            }
            TransferObj transferObj = new TransferObj
            {
                Action = action, Arg = objectAsJson
            };
            await SendTransferObjectToClient(stream, transferObj);

        }

        public static async Task SendToClientNoValueAsync(NetworkStream stream)
        {
            string action = "RETURN";
            
            TransferObj transferObj = new TransferObj
            {
                Action = action
            };
            await SendTransferObjectToClient(stream, transferObj);
        }

        private static async Task SendTransferObjectToClient(NetworkStream stream, TransferObj transferObj)
        {
            string transferAsJson = JsonSerializer.Serialize(transferObj);
            byte[] toServer = Encoding.UTF8.GetBytes(transferAsJson);
            await stream.WriteAsync(toServer, 0, toServer.Length);
        }

        public static async Task SendExceptionToClientAsync(NetworkStream stream, Exception exception)
        {
            Error error = new Error(exception);
            await SendToClientWithValueAsync(stream, error);
        }
    }
}