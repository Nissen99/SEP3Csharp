using System;
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
            ValidateInput(user);
            await userNetworking.RegisterUser(user);
        }

        public async Task<User> ValidateUser(User user)
        {
            User toReturn;
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

        private void ValidateInput(User user)
        {
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Password))
                throw new ArgumentException("Field is missing");
        }


    }
}