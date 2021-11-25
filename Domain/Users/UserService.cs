using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Domain.Users
{
    public class UserService : IUserService
    {
        private IUserNetworking userNetworking;
        public UserService(IUserNetworking userNetworking)
        {
            this.userNetworking = userNetworking;
        }

        public async Task RegisterUser(User user)
        {
            await userNetworking.RegisterUser(user);
        }

        public async Task<User> ValidateUser(User user)
        {
            User userToReturn = await userNetworking.ValidateUser(user);
            return userToReturn;
        }
        
        
    }
}