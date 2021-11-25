using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Domain.Library;
using Domain.Play;
using Domain.SongSearch;
using Domain.Users;

namespace SocketsT1_T2.Tier2
{
    public class Server : IServer
    {
        private ILibraryService libraryService;
        private IPlayService playSongService;
        private ISongSearchService songSearchService;
        private IUserService userService;
        public Server(ILibraryService libraryService, IPlayService playSongService, ISongSearchService songSearchService, IUserService userService)
       {
           this.libraryService = libraryService;
           this.playSongService = playSongService;
           this.songSearchService = songSearchService;
           this.userService = userService;
       }

       public void startServer()
        {
            IPAddress ip = IPAddress.Any;
            TcpListener listener = new TcpListener(ip, 1098);
            listener.Start();
            Console.WriteLine("SERVER STARED");
            
            libraryService.SendSongListToDBAsync();
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                IClientHandler clientHandler = new ClientHandler(client, playSongService, userService, songSearchService);
                new Thread(clientHandler.ListenToClientAsync).Start();
            }
            
        }

    }

    
}