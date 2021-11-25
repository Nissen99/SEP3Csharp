using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Domain.Users
{
    public interface IUserService
    {
        public Task RegisterUser(User user);
        public Task<User> ValidateUser(User user);
    }
}