using System;
using System.Threading.Tasks;
using Domain.Util;
/*
 * Denne klasse håndterer User relaterede handlinger.
 */
namespace Domain.User
{
    public class UserService : IUserService
    {
        private IUserNetworking userNetworking;

        public UserService(IUserNetworking userNetworking)
        {
            this.userNetworking = userNetworking;
        }

        public async Task RegisterUser(Entities.User user)
        {
            if (!InputValidator.ValidateUser(user)) throw new ArgumentException("User not Valid, something missing");
            
            await userNetworking.RegisterUser(user);
        }

        public async Task<Entities.User> ValidateUser(Entities.User user)
        {
            if (!InputValidator.ValidateUser(user)) throw new ArgumentException("Some property not found");
            
            return await userNetworking.ValidateUser(user);
        }
    }
}