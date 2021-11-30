using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace RestT2_T3
{
    public class HttpClientBase
    {
        protected readonly string Uri = "http://localhost:8080/";

    
        protected async Task<T> HandleResponseFromServer<T>(HttpResponseMessage responseMessage)
        {
            if (!responseMessage.IsSuccessStatusCode)
            {
                Console.WriteLine("Not good");
                throw new Exception($"Error: {responseMessage.StatusCode}, " +
                                    $"{responseMessage.ReasonPhrase}");
            }

            string inFromServerJson = await responseMessage.Content.ReadAsStringAsync();

            T inFromServer = JsonSerializer.Deserialize<T>(inFromServerJson,
                new JsonSerializerOptions() {PropertyNameCaseInsensitive = true});

            return inFromServer;

        }
        
    }
    }
