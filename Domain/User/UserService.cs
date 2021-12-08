using System;
using System.Threading.Tasks;

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
            ValidateInput(user);
            await userNetworking.RegisterUser(user);
        }

        public async Task<Entities.User> ValidateUser(Entities.User user)
        {
            Entities.User toReturn;
            ValidateInput(user);
            try
            {
                toReturn = await userNetworking.ValidateUser(user);

            }
            catch (Exception e)
            {
                throw;
            }
            return toReturn;
        }

        private void ValidateInput(Entities.User user)
        {
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
                throw new ArgumentException("Field is missing");
        }


    }
}