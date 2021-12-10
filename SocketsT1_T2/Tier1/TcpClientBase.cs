using System;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Entities;
using SocketsT1_T2.Shared;

namespace SocketsT1_T2.Tier1
{
    public class TcpClientBase
    {
        private readonly string hostName = "localhost";
        private readonly int portNumber = 1098;
        
        
        protected async Task SendServerRequest<T>(string action, T TObject, TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            string TObjectAsJson = JsonSerializer.Serialize(TObject);
            TransferObj transferObj = new TransferObj()
            {
                Action = action, Arg = TObjectAsJson
            };
            string transferAsJson = JsonSerializer.Serialize(transferObj);
            byte[] toServer = Encoding.UTF8.GetBytes(transferAsJson);
            await stream.WriteAsync(toServer);
        }

        protected async Task ServerResponseCheckForException(TcpClient client, int bufferSize)
        {
            NetworkStream stream = client.GetStream();

            byte[] dataFromServer = new byte[bufferSize];
            int bytesRead = await stream.ReadAsync(dataFromServer, 0, dataFromServer.Length);

            string inFromServer = Encoding.UTF8.GetString(dataFromServer, 0, bytesRead);
            
            TransferObj objectFromServer = JsonSerializer.Deserialize<TransferObj>(inFromServer,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            checkAndHandleException(objectFromServer);
        }

     


        protected async Task<T> ServerResponse<T>(TcpClient client, int bufferSize)
        {
            NetworkStream stream = client.GetStream();

            byte[] dataFromServer = new byte[bufferSize];
            int bytesRead = await stream.ReadAsync(dataFromServer, 0, dataFromServer.Length);

            string inFromServer = Encoding.UTF8.GetString(dataFromServer, 0, bytesRead);
            
            return returnFromServer<T>(inFromServer);
        }
        
        private T returnFromServer<T>(string inFromServer)
        {
            TransferObj objectFromServer = JsonSerializer.Deserialize<TransferObj>(inFromServer,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            checkAndHandleException(objectFromServer);

            T returnFromServer = JsonSerializer.Deserialize<T>(objectFromServer.Arg);

            return returnFromServer;
        }

        private void checkAndHandleException(TransferObj objectFromServer)
        {
            if (objectFromServer.Action.Equals("Exception"))
            {
                Error error = JsonSerializer.Deserialize<Error>(objectFromServer.Arg);
                Console.WriteLine($"{error.TimeStamp} \n {error.StackTrace}");
                throw new Exception($"{error.Message}");
            }
        }
        
        
        protected TcpClient GetTcpClient()
        {
            return new TcpClient(hostName, portNumber);
        }
    }
}