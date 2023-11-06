using InterfaceComposition.REST.RestRequests.Definitions;
using InterfaceComposition.REST.UrlParameters;
using System;
using System.Net.Http;

namespace InterfaceComposition.REST.RestRequests.Builders
{
    public class NonPayloadRestRequestBuilder : BaseRestRequestBuilder<NonPayloadRestRequest>
    {
        public NonPayloadRestRequestBuilder() : base()
        {
        }

        public NonPayloadRestRequestBuilder(
            HttpMethod method,
            string? relativePath,
            UrlParameterList? urlParameter)
            : base(method, relativePath, urlParameter)
        {
        }

        protected override void ValidateRequestMethod()
        {
            switch (MethodName)
            {
                case "POST":
                case "PUT":
                case "PATCH":
                    throw new ArgumentException("POST/PUT/PATCH requests must have payloads");
            }
        }

        protected override NonPayloadRestRequest BuildRequestInternal()
        {
            if (!HasMethod) throw new InvalidOperationException("Method must be set before building request");
            return new(
                Method,
                RelativePath,
                UrlParameters);
        }
    }
}
