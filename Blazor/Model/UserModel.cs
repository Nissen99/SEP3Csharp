using System.Threading.Tasks;
using Entities;
using SocketsT1_T2.Tier1;

namespace Blazor.Model
{
    public class UserModel : IUserModel
    {
        private IClient client;

        public UserModel(IClient client)
        {
            this.client = client;
        }


        public async Task<User> ValidateUser(User user)
        {
            return await client.validateUser(user);
           
        }

        public async Task RegisterUser(User user) => await client.RegisterUser(user);
    }
}