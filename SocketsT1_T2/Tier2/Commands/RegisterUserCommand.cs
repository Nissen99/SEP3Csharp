using System.Net.Sockets;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Users;
using Entities;
using RestT2_T3;
using SocketsT1_T2.Tier2.Util;

namespace SocketsT1_T2.Tier2.Commands
{
    public class RegisterUserCommand: ICommand
    {
        private IUserService userService = new UserService(new UserRestClient());
        public async Task Execute(NetworkStream stream, JsonElement tObj)
        {
            User user = JsonElementConverter.ElementToObject<User>(tObj);
            await userService.RegisterUser(user);
        }
    }
}