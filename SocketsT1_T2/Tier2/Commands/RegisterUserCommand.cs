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
        private TransferObj requestObj;
        

        public RegisterUserCommand(TransferObj requestObj)
        {
            userService = ServicesFactory.GetUserService();
            this.requestObj = requestObj;
        }

        public async Task<TransferObj> Execute()
        {
            try
            {
                User user = JsonElementConverter.ElementToObject<User>(requestObj.Arg);
                await userService.RegisterUser(user);
                return await ServerResponse.PrepareTransferObjectNoValueAsync();
            }
            catch (Exception e)
            {
                return await ServerResponse.PrepareTransferObjectWithExceptionAsync(e);

            }
            
        }
    }
}