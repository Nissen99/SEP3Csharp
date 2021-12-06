using System;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Play;
using Entities;
using RestT2_T3;
using SocketsT1_T2.Tier2.Util;

namespace SocketsT1_T2.Tier2.Commands
{
    public class PlaySongCommand : ICommand
    {
        private IPlayService playService = new PlayService(new PlayRestClient());
        public async Task Execute(NetworkStream stream, string argFromTransfer)
        {
            try
            {
            Song tObjSong = JsonElementConverter.ElementToObject<Song>(argFromTransfer);
            byte[] song = await playService.PlayAsync(tObjSong);
            await ServerResponse.SendToClientWithValueAsync(stream, song);
            }
            catch (Exception e)
            {
                await ServerResponse.SendExceptionToClientAsync(stream, e);
            }
          
        }
    }
}