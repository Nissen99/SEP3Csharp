using System.Threading.Tasks;
using Entities;

namespace Blazor.Model
{
    public interface IUserModel
    {
        Task<User> ValidateUser(User user);

        Task RegisterUser(User user);
    }
}