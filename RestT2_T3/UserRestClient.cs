using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Users;
using Entities;

namespace RestT2_T3
{
    public class UserRestClient : IUserNetworking
    {
        private string uri = "http://localhost:8080/";
        public async Task RegisterUser(User user)
        {
            using HttpClient client = new HttpClient();
            string userAsJSon = JsonSerializer.Serialize(user,
                new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            StringContent content = new StringContent(userAsJSon, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(uri + "users", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($@"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }
        public Task<User> ValidateUser(User user)
        {
            throw new NotImplementedException();
        }

        private async Task<string> ResponseFromServer(HttpResponseMessage responseMessage)
        {
            RequestCodeCheck(responseMessage);

            string responseFromServer = await responseMessage.Content.ReadAsStringAsync();
            return responseFromServer;
        }
        private void RequestCodeCheck(HttpResponseMessage responseMessage)
        {
            Console.WriteLine("Checking request");
            if (!responseMessage.IsSuccessStatusCode)
            {
                Console.WriteLine("Not good");
                throw new Exception($"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
            }
        }
    }
    
}