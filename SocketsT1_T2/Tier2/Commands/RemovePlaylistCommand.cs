using System;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Play;
using Domain.Playlist;
using Entities;
using RestT2_T3;
using SocketsT1_T2.Tier2.Util;

namespace SocketsT1_T2.Tier2.Commands
{
    public class RemovePlaylistCommand:ICommand
    
    {
        private IPlayListService playListService = new PlayListService(new PlaylistRestClient());

        public async Task Execute(NetworkStream stream, string argFromTransfer)
        {
            try
            {
                Playlist playlist = JsonElementConverter.ElementToObject<Playlist>(argFromTransfer);
                await playListService.DeletePlayListAsync(playlist);
                await ServerResponse.SendToClientNoValueAsync(stream);
            }
            catch (Exception e)
            {
                await ServerResponse.SendExceptionToClientAsync(stream, e);
            }
            

        }
    }
}