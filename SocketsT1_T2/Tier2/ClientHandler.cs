using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Album;
using Domain.Artist;
using Domain.Play;
using Domain.SongSearch;
using Domain.Users;
using Entities;
using SocketsT1_T2.Shared;
using SocketsT1_T2.Tier2.Commands;
using SocketsT1_T2.Tier2.Util;

namespace SocketsT1_T2.Tier2
{
    public class ClientHandler : IClientHandler
    {
        private TcpClient client;
        private TransferObj requestObject;

        public ClientHandler(TcpClient client)
        {
            this.client = client;
        }
        
        public async void ListenToClientAsync()
        {
            Console.WriteLine("LISTEN");
            requestObject = await GetRequestObjAsync();
            RequestHandler rHandler = new RequestHandler(client.GetStream(), requestObject);
            await rHandler.ExecuteCommand();
            
            ICommand command = rHandler.GetCommand();
            TransferObj responseObj = command.ResponseObj;
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