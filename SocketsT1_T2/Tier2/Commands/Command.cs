using System;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Play;
using Entities;

namespace SocketsT1_T2.Tier2.Commands
{
    public interface ICommand
    {
        Task Execute(NetworkStream stream, JsonElement tObj);
    }
}