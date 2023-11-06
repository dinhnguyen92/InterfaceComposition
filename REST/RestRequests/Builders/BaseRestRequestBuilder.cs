using InterfaceComposition.REST.RestRequests.Definitions;
using InterfaceComposition.REST.UrlParameters;
using System.Diagnostics.CodeAnalysis;

namespace InterfaceComposition.REST.RestRequests.Builders
{
    public abstract class BaseRestRequestBuilder<TRequest> where TRequest : BaseRestRequest
    {
        protected HttpMethod? Method { get; set; }
        protected string? RelativePath { get; set; }
        protected UrlParameterList UrlParameters { get; set; }
        protected string? MethodName => Method?.Method;

        [MemberNotNullWhen(true, nameof(Method))]
        [MemberNotNullWhen(true, nameof(MethodName))]
        protected bool HasMethod => Method != null;

        public BaseRestRequestBuilder()
        {
            Method = null;
            RelativePath = null;
            UrlParameters = new();
        }

        public BaseRestRequestBuilder(
            HttpMethod method,
            string? relativePath,
            UrlParameterList? urlParameter)
        {
            Method = method;
            RelativePath = relativePath;
            UrlParameters = urlParameter ?? new();
            ValidateRequestMethod();
        }

        protected virtual void ResetBuilder()
        {
            Method = null;
            RelativePath = null;
            UrlParameters = new();
        }

        protected abstract void ValidateRequestMethod();

        public void SetMethod(HttpMethod method)
        {
            Method = method;
            ValidateRequestMethod();
        }

        public void SetRelativePath(string? relativePath)
        {
            RelativePath = relativePath;
        }

        public void AddUrlParameters(UrlParameterList urlParameters)
        {
            UrlParameters.AddRange(urlParameters);
        }

        protected abstract TRequest BuildRequestInternal();

        public TRequest Build()
        {
            var request = BuildRequestInternal();
            ResetBuilder();
            return request;
        }
    }
}
