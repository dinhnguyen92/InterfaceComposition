using InterfaceComposition.REST.Payload.Serialization.Bson;
using InterfaceComposition.REST.UrlParameters;
using System.Net.Http;

namespace InterfaceComposition.REST.RestRequests.Builders
{
    public class BsonPayloadRestRequestBuilder : BasePayloadRestRequestBuilder
    {
        public BsonPayloadRestRequestBuilder() : base()
        {
        }

        public BsonPayloadRestRequestBuilder(
            HttpMethod method,
            string? relativePath,
            UrlParameterList? urlParameter)
            : base(method, relativePath, urlParameter)
        {
        }

        protected override HttpContent SerializeToPayloadContent<TPayload>(TPayload payload)
        {
            return BsonContentHelper.SerializeToBsonByteArray(payload);
        }
    }
}
