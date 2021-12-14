using System.Text.Json;


/*
 * Hjælpeklasse der bruges til udpakning af requests fra klienten.
 */
namespace SocketsT1_T2.Tier2.Util
{
    public class JsonElementConverter
    {
        public static T ElementToObject<T>(string element)
        {
            return JsonSerializer.Deserialize<T>(element,new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
        }
    }
}
