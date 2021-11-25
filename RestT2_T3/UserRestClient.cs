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
            StringContent content = new StringContent(userAsJSon, Encoding.ASCII, "application/json");
            HttpResponseMessage response = await client.PostAsync(uri + "users", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($@"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }
        public async Task<User> ValidateUser(User user)
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage responseMessage =
                await client.GetAsync(uri + $"users/{user.Username}&{user.Password}");

            User test = await ResponseFromServer(responseMessage);
            Console.WriteLine($"{test.Username} {test.Password} {test.Role}");
            return test;
        }

        private async Task<User> ResponseFromServer(HttpResponseMessage responseMessage)
        {
            RequestCodeCheck(responseMessage);

            string responseFromServer = await responseMessage.Content.ReadAsStringAsync();
            User toReturn = JsonSerializer.Deserialize<User>(responseFromServer, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return toReturn;
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