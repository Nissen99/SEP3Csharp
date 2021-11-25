using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace Domain.Users
{
    public class UserService : IUserService
    {
        private IUserNetworking userNetworking;
        public IList<User> Users { get; }
        public UserService(IUserNetworking userNetworking)
        {
            this.userNetworking = userNetworking;
        }

        public Task<IList<User>> GetUsers()
        {
            throw new System.NotImplementedException();
        }

        public Task<User> AddUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveUser(User user)
        {
            throw new System.NotImplementedException();
        }

        
        public User ValidateUser(string userName, string password)
        {
            throw new System.NotImplementedException();
        }
        
        
    }
}