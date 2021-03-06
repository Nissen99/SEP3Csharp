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
 * Commando klasse. Den klasse styrer udpakningen af handlingen 'Register User'.
 * Den sender det udpakkede objekt til sin receiver IPlaylistService og returnerer en respons.
 */

namespace SocketsT1_T2.Tier2.Commands
{
    [MyCommand]
    public class RegisterUserCommand: ICommand
    {
        private IUserService userService;
        public TransferObj RequestObj { get; set; }
        public string Action { get; }

        public RegisterUserCommand()
        {
            Action = "REGISTERUSER";
            userService = ServicesFactory.GetUserService();
        }

        public async Task<TransferObj> Execute()
        {
            try
            {
                User user = JsonElementConverter.ElementToObject<User>(RequestObj.Arg);
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
