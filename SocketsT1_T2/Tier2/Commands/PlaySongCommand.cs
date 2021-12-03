using System;
using System.Net.Sockets;
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
            Song tObjSong = JsonElementConverter.ElementToObject<Song>(argFromTransfer);
            Song song = await playService.PlayAsync(tObjSong);
            await ServerResponse.SendToClient<Song>(stream, song);
        }
    }
}