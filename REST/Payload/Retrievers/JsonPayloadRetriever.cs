using InterfaceComposition.ApiClients;
using InterfaceComposition.REST.Payload.Serialization.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace InterfaceComposition.REST.Payload.Retrievers
{
    public class JsonPayloadRetriever<TPayload> :
        BasePayloadRetriever<TPayload>
        where TPayload : class
    {
        public JsonPayloadRetriever(HttpRestClient restClient) : base(restClient)
        {
        }

        protected override Task<TPayload> ParseHttContentForPayloadAsync(HttpContent content)
        {
            return JsonContentHelper.DeserializeHttpContentAsJsonAsync<TPayload>(content);
        }
    }
}
