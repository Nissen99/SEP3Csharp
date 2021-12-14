using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

/*
 * Dette er server opstarten. Her oprettes en TcpListener som alle klienter skal tilgå. 
 * Klienter bliver accepteret i et while loop og bliver knyttet til hver deres Client Handler.
 */
namespace SocketsT1_T2.Tier2
{
    public class Server : IServer
    {
        
       public void startServer()
        {
            IPAddress ip = IPAddress.Any;
            TcpListener listener = new TcpListener(ip, 1098);
            listener.Start();
            Console.WriteLine("SERVER STARED");
            
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                IClientHandler clientHandler = new ClientHandler(client);
                new Thread(clientHandler.ListenToClientAsync).Start();
            }
        }

    }

    
}
