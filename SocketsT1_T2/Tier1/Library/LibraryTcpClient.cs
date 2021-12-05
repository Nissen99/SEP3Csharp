﻿using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using Entities;

namespace SocketsT1_T2.Tier1.Library
{
    public class LibraryTcpClient : TcpClientBase, ILibraryNetworkClient
    {
        public async Task<IList<Entities.Song>> GetAllSongs()
        {
            using TcpClient client = GetTcpClient();
            await SendServerRequest("GETSONGS", "", client);
            return await ServerResponse<IList<Entities.Song>>(client, 100000);
        }

        public async Task AddNewSongAsync(Entities.Song newSong, Mp3 mp3)
        {
            using TcpClient client = GetTcpClient();
            object[] toSent = {newSong, mp3};
            await SendServerRequest("UPLOADSONG", toSent, client);
        }

        public async Task RemoveSongAsync(Entities.Song song)
        {
            using TcpClient client = GetTcpClient();
            await SendServerRequest("REMOVESONG", song, client);
        }

    }
}