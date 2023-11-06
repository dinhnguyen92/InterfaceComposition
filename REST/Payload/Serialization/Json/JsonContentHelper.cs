using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace InterfaceComposition.REST.Payload.Serialization.Json
{
    public static class JsonContentHelper
    {
        public static StringContent SerializeToJsonStringContent<Payload>(Payload payload)
        {
            return new StringContent(
                Encoding.UTF8.GetString(JsonSerializer.SerializeToUtf8Bytes(payload)),
                Encoding.UTF8,
                RESTConstants.JSON_MEDIA_TYPE);
        }

        public static async Task<T> DeserializeHttpContentAsJsonAsync<T>(HttpContent httpContent)
        {
            return await httpContent.ReadFromJsonAsync<T>() ?? throw new Exception("Failed to read response message");
        }

        public static async Task<T> DeserializeStreamAsJsonAsync<T>(Stream stream)
        {
            return await JsonSerializer.DeserializeAsync<T>(stream) ?? throw new Exception("Failed to deserialize stream");
        }
    }
}
