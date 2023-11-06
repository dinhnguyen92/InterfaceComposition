using InterfaceComposition.REST.UrlParameters;
using System.Diagnostics.CodeAnalysis;

namespace InterfaceComposition.REST.RestRequests.Definitions
{
    public abstract class BaseRestRequest
    {
        public HttpMethod Method { get; }
        public string? RelativePath { get; }
        public UrlParameterList? UrlParameters { get; }
        public string MethodName => Method.Method;

        [MemberNotNullWhen(true, nameof(RelativePath))]
        public bool HasRelativePath => !string.IsNullOrEmpty(RelativePath);

        [MemberNotNullWhen(true, nameof(UrlParameters))]
        public bool HasUrlParameters => UrlParameters != null && UrlParameters.Any();

        public string RequestUri
        {
            get
            {
                string requestUri = RelativePath ?? string.Empty;
                if (HasUrlParameters)
                {
                    requestUri += $"?{UrlParameters.ToUrlEncodedString()}";
                }
                return requestUri;
            }
        }


        public BaseRestRequest(
            HttpMethod method,
            string? relativePath,
            UrlParameterList? urlParameters)
        {
            Method = method;
            RelativePath = relativePath;
            UrlParameters = urlParameters;
        }
    }
}
