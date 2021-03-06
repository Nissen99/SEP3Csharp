using System.Net.Http;
using System.Threading.Tasks;
using Domain.User;
using Entities;
 /*
  * Denne klasse står REST kommunikationen af User relaterede requests
  */
namespace RestT2_T3
{
    public class UserRestClient : HttpClientBase, IUserNetworking
    {
        public async Task RegisterUser(User user)
        {
            using HttpClient client = new HttpClient();
            
            StringContent content = FromObjectToStringContentCamelCase(user);
            
            HttpResponseMessage responseMessage = await client.PostAsync(Uri + "user", content);
            
            HandleResponseNoReturn(responseMessage);
        }
        
        public async Task<User> ValidateUser(User user)
        {
            using HttpClient client = new HttpClient();
            
            HttpResponseMessage responseMessage =
                await client.GetAsync(Uri + $"user?username={user.Username}&password={user.Password}");

            return await HandleResponseGet<User>(responseMessage);
        }
        
    }
}