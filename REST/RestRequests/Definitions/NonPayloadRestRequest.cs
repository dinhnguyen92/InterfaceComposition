using InterfaceComposition.REST.UrlParameters;

namespace InterfaceComposition.REST.RestRequests.Definitions
{
    public class NonPayloadRestRequest : BaseRestRequest
    {
        public NonPayloadRestRequest(
            HttpMethod method,
            string? relativePath,
            UrlParameterList? urlParameters)
            : base(method, relativePath, urlParameters)
        {
            switch (MethodName)
            {
                case "POST":
                case "PUT":
                case "PATCH":
                    throw new ArgumentException("POST/PUT/PATCH requests must have payloads");
            }
        }
    }
}
