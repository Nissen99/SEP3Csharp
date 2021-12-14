using System.Threading.Tasks;
using Entities;
using SocketsT1_T2.Tier1.User;

/*
 * Klassen fungere gateway fra mvvm del til resten af systemet, her gennem kald til Client
 */
namespace Blazor.Model.UserModel
{
    public class UserModel : IUserModel
    {
        private IUserNetworkClient userClient;

        public UserModel(IUserNetworkClient userClient)
        {
            this.userClient = userClient;
        }


        public async Task<User> ValidateUser(User user)
        {
            return await userClient.ValidateUser(user);
           
        }

        public async Task RegisterUser(User user)
        {
            await userClient.RegisterUser(user);
        }
    }
}