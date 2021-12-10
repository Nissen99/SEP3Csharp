using System;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SocketsT1_T2.Shared;
using SocketsT1_T2.Tier2.Util;

namespace SocketsT1_T2.Tier2
{
    public class ClientHandler : IClientHandler
    {
        private TcpClient client;
        public ClientHandler(TcpClient client)
        {
            this.client = client;
        }
        
        public async void ListenToClientAsync()
        {
            Console.WriteLine("LISTEN");
            TransferObj requestObject = await GetRequestObjAsync();
            IRequestHandler rHandler = new RequestHandler(requestObject);
            TransferObj responseObj = await rHandler.ExecuteCommand();
            await SendTransferObjectToClient(client.GetStream(), responseObj);
            client.Dispose();
        }
        private async Task<TransferObj> GetRequestObjAsync()
        {
            byte[] dataFromServer = new byte[30000000];
            int bytesRead = await client.GetStream().ReadAsync(dataFromServer, 0, dataFromServer.Length);
            string readFromClient = Encoding.UTF8.GetString(dataFromServer, 0, bytesRead);
            TransferObj transferObj = JsonSerializer.Deserialize<TransferObj>(readFromClient,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                });
            return transferObj;
        }
        
        private async Task SendTransferObjectToClient(NetworkStream stream, TransferObj transferObj)
        {
            string transferAsJson = JsonSerializer.Serialize(transferObj);
            byte[] toServer = Encoding.UTF8.GetBytes(transferAsJson);
            await stream.WriteAsync(toServer, 0, toServer.Length);
        }
        
    }
}