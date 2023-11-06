using InterfaceComposition.ApiClients;
using InterfaceComposition.REST.RestRequests.Builders;

namespace InterfaceComposition.REST.Payload.Senders
{
    public class JsonPayloadSender : BasePayloadSender
    {
        public JsonPayloadSender(HttpRestClient restClient) : base(restClient)
        {
        }

        protected override BasePayloadRestRequestBuilder InitializeRequestBuilderWithPayload<TPayLoad>(TPayLoad payload)
        {
            var requestBuilder = new JsonPayloadRESTRequestBuilder();
            requestBuilder.SetPayload(payload);
            return requestBuilder;
        }
    }
}
