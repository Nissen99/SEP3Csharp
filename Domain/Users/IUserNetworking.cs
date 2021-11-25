using System.Threading.Tasks;
using Entities;

namespace Domain.Users
{
    public interface IUserNetworking
    {       
        Task RegisterUser(User user);
        Task<User> ValidateUser(User user);

    }
}