using InterfaceComposition.REST.Payload.Serialization.Json;
using InterfaceComposition.REST.UrlParameters;

namespace InterfaceComposition.REST.RestRequests.Builders
{
    public class JsonPayloadRESTRequestBuilder : BasePayloadRestRequestBuilder
    {
        public JsonPayloadRESTRequestBuilder() : base()
        {
        }

        public JsonPayloadRESTRequestBuilder(
            HttpMethod method,
            string? relativePath,
            UrlParameterList? urlParameter)
            : base(method, relativePath, urlParameter)
        {
        }

        protected override HttpContent SerializeToPayloadContent<TPayload>(TPayload payload)
        {
            return JsonContentHelper.SerializeToJsonStringContent(payload);
        }
    }
}
