using System.Threading.Tasks;

namespace Domain.User
{
    public interface IUserService
    {
        public Task RegisterUser(Entities.User user);
        public Task<Entities.User> ValidateUser(Entities.User user);
    }
}