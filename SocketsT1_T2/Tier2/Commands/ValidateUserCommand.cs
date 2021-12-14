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

/*
 * Commando klasse. Den klasse styrer udpakningen af handlingen 'Validate User'.
 * Den sender det udpakkede objekt til sin receiver IUserService og returnerer en respons.
 */


namespace SocketsT1_T2.Tier2.Commands
{
    public class ValidateUserCommand: ICommand
    {
        private IUserService userService;
        private TransferObj requestObj;

        public ValidateUserCommand(TransferObj requestObj)
        {
            userService = ServicesFactory.GetUserService();
            this.requestObj = requestObj;
        }

        public async Task<TransferObj> Execute()
        {
            try
            {
                User user = JsonElementConverter.ElementToObject<User>(requestObj.Arg);
                User toReturn = await userService.ValidateUser(user);
                return await ServerResponse.PrepareTransferObjectWithValueAsync(toReturn);
            }
            catch (Exception e)
            {
                return await ServerResponse.PrepareTransferObjectWithExceptionAsync(e);

            }
            
        }
    }
}
