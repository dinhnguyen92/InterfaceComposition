using InterfaceComposition.REST.RestRequests.Definitions;
using InterfaceComposition.REST.UrlParameters;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;

namespace InterfaceComposition.REST.RestRequests.Builders
{
    public abstract class BasePayloadRestRequestBuilder : BaseRestRequestBuilder<PayloadRestRequest>
    {
        protected HttpContent? PayloadContent { get; set; }

        [MemberNotNullWhen(true, nameof(PayloadContent))]
        protected bool HasPayloadContent => PayloadContent != null;


        public BasePayloadRestRequestBuilder() : base()
        {
        }

        public BasePayloadRestRequestBuilder(
            HttpMethod method,
            string? relativePath,
            UrlParameterList? urlParameter)
            : base(method, relativePath, urlParameter)
        {
            PayloadContent = null;
        }

        public BasePayloadRestRequestBuilder(
            HttpMethod method,
            string? relativePath,
            UrlParameterList? urlParameter,
            HttpContent? payloadContent)
            : base(method, relativePath, urlParameter)
        {
            PayloadContent = payloadContent;
        }


        protected override void ValidateRequestMethod()
        {
            switch (MethodName)
            {
                case "GET":
                case "DELETE":
                    throw new ArgumentException("GET/DELETE requests must not have payloads");
            }
        }

        protected abstract HttpContent SerializeToPayloadContent<TPayload>(TPayload payload);

        public void SetPayload<TPayload>(TPayload payload)
        {
            PayloadContent = SerializeToPayloadContent(payload);
        }

        public void SetPayloadContent(HttpContent payloadContent)
        {
            PayloadContent = payloadContent;
        }

        protected override PayloadRestRequest BuildRequestInternal()
        {
            if (!HasMethod) throw new InvalidOperationException("Method must be set before building request");
            if (!HasPayloadContent) throw new InvalidOperationException("Payload content must be set before building request");
            return new(
                Method,
                RelativePath,
                UrlParameters,
                PayloadContent);
        }

        protected override void ResetBuilder()
        {
            base.ResetBuilder();
            PayloadContent = null;
        }
    }
}
