using System.Text.Json;

namespace SocketsT1_T2.Tier2.Util
{
    public class JsonElementConverter
    {
        public static T ElementToObject<T>(JsonElement element)
        {
            string stringElement = element.GetRawText();
            return JsonSerializer.Deserialize<T>(stringElement,new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
        }
    }
}