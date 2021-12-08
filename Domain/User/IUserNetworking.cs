using System.Threading.Tasks;

namespace Domain.User
{
    public interface IUserNetworking
    {       
        Task RegisterUser(Entities.User user);
        Task<Entities.User> ValidateUser(Entities.User user);

    }
}