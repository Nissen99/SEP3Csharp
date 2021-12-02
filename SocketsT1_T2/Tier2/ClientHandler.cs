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
using Domain.Songs;
using Domain.SongSearch;
using Domain.Users;
using Entities;
using SocketsT1_T2.Tier2.Commands;
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

            RequestHandler rHandler = new RequestHandler(client.GetStream());
            ICommand command = await rHandler.GetCommand();
            await command.Execute(client.GetStream(),rHandler.RequestArg );
            
            client.Dispose();
        }
        
    }
}