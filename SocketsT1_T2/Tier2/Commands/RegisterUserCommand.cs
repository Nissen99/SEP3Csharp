using System;
using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.User;
using Entities;
using Factory;
using RestT2_T3;
using SocketsT1_T2.Shared;
using SocketsT1_T2.Tier2.Util;

namespace SocketsT1_T2.Tier2.Commands
{
    public class RegisterUserCommand: ICommand
    {
        private IUserService userService;
        private NetworkStream stream;
        private TransferObj requestObj;
        
        public TransferObj ResponseObj { get; private set; }

        public RegisterUserCommand(NetworkStream stream, TransferObj requestObj)
        {
            userService = ServicesFactory.GetUserService();
            this.stream = stream;
            this.requestObj = requestObj;
        }

        public async Task Execute()
        {
            try
            {
                User user = JsonElementConverter.ElementToObject<User>(requestObj.Arg);
                await userService.RegisterUser(user);
                ResponseObj = await ServerResponse.PrepareTransferObjectNoValueAsync();
            }
            catch (Exception e)
            {
                ResponseObj = await ServerResponse.SendExceptionToClientAsync(e);

            }
            
        }
    }
}