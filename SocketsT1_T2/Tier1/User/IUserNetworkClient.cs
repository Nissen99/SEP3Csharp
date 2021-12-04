using System.Threading.Tasks;

namespace SocketsT1_T2.Tier1.User
{
    public interface IUserNetworkClient
    {
        Task RegisterUser(Entities.User user);
        Task<Entities.User> ValidateUser(Entities.User user);
    }
}