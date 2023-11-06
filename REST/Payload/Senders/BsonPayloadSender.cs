using InterfaceComposition.ApiClients;
using InterfaceComposition.REST.RestRequests.Builders;

namespace InterfaceComposition.REST.Payload.Senders
{
    public class BsonPayloadSender : BasePayloadSender
    {
        public BsonPayloadSender(HttpRestClient restClient) : base(restClient)
        {
        }

        protected override BasePayloadRestRequestBuilder InitializeRequestBuilderWithPayload<TPayLoad>(TPayLoad payload)
        {
            var requestBuilder = new BsonPayloadRestRequestBuilder();
            requestBuilder.SetPayload(payload);
            return requestBuilder;
        }
    }
}
