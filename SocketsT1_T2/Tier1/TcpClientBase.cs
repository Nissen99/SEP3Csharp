﻿using System;
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


        protected async Task<T> serverResponse<T>(TcpClient client, int bufferSize)
        {
            NetworkStream stream = client.GetStream();

            byte[] dataFromServer = new byte[bufferSize];
            int bytesRead = await stream.ReadAsync(dataFromServer, 0, dataFromServer.Length);

            string inFromServer = Encoding.UTF8.GetString(dataFromServer, 0, bytesRead);
            
            return ReturnFromServer<T>(inFromServer);
        }
        
        private T ReturnFromServer<T>(string inFromServer)
        {
            TransferObj objectFromServer = JsonSerializer.Deserialize<TransferObj>(inFromServer,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (objectFromServer.Action.Equals("Exeption"))
            {
                Exception exception = JsonSerializer.Deserialize<Exception>(objectFromServer.Arg);
                throw exception;
            }

            T returnFromServer = JsonSerializer.Deserialize<T>(objectFromServer.Arg);

            return returnFromServer;
        }

        
        
        protected TcpClient GetTcpClient()
        {
            return new TcpClient(hostName, portNumber);
        }
    }
}