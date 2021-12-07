using System;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Users;
using Entities;
using Factory;
using RestT2_T3;
using SocketsT1_T2.Tier2.Util;

namespace SocketsT1_T2.Tier2.Commands
{
    public class ValidateUserCommand: ICommand
    {
        private IUserService userService = ServicesFactory.GetUserService();
        public async Task Execute(NetworkStream stream, string argFromTransfer)
        {
            try
            {
                User user = JsonElementConverter.ElementToObject<User>(argFromTransfer);
                User toReturn = await userService.ValidateUser(user);
                await ServerResponse.SendToClientWithValueAsync(stream, toReturn);
            }
            catch (Exception e)
            {
                await ServerResponse.SendExceptionToClientAsync(stream, e);

            }
            
        }
    }
}