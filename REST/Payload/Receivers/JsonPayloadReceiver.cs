using InterfaceComposition.ApiClients;
using InterfaceComposition.REST.Payload.Serialization.Json;

namespace InterfaceComposition.REST.Payload.Receivers
{
    public class JsonPayloadReceiver<TPayload> :
        BasePayloadReceiver<TPayload>
        where TPayload : class
    {
        public JsonPayloadReceiver(HttpRestClient restClient) : base(restClient)
        {
        }

        protected override Task<TPayload> ParseHttContentForPayloadAsync(HttpContent content)
        {
            return JsonContentHelper.DeserializeHttpContentAsJsonAsync<TPayload>(content);
        }
    }
}
