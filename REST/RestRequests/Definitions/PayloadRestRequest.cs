using InterfaceComposition.REST.UrlParameters;

namespace InterfaceComposition.REST.RestRequests.Definitions
{
    public class PayloadRestRequest : BaseRestRequest
    {
        public HttpContent PayloadContent { get; }

        public PayloadRestRequest(
            HttpMethod method,
            string? relativePath,
            UrlParameterList? urlParameters,
            HttpContent payloadContent)
            : base(method, relativePath, urlParameters)
        {
            switch (MethodName)
            {
                case "GET":
                case "DELETE":
                    throw new ArgumentException("GET/DELETE requests must not have payloads");
            }

            PayloadContent = payloadContent;
        }
    }
}
