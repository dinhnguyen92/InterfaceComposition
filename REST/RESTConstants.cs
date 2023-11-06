using Microsoft.AspNetCore.Http;

namespace InterfaceComposition.REST
{
    public class RESTConstants
    {
        public const string JSON_MEDIA_TYPE = "application/json";
        public const string BSON_MEDIA_TYPE = "application/bson";
        public const string STREAM_MEDIA_TYPE = "application/octet-stream";

        public const string AUTHORIZATION_HEADER_NAME = "Authorization";
        public const string AUTHORIZATION_TOKEN_PREFIX = "Bearer ";

        public static KeyValuePair<string, string> BuildAuthorizationHeader(string token)
        {
            return new KeyValuePair<string, string>(AUTHORIZATION_HEADER_NAME, $"{AUTHORIZATION_TOKEN_PREFIX}{token}");
        }

        public static string? GetAuthorizationToken(IHeaderDictionary headers)
        {
            if (headers.ContainsKey(AUTHORIZATION_HEADER_NAME))
            {
                string? authHeaderValue = headers[AUTHORIZATION_HEADER_NAME];
                if (!string.IsNullOrEmpty(authHeaderValue) && authHeaderValue.StartsWith(AUTHORIZATION_TOKEN_PREFIX))
                {
                    return authHeaderValue[AUTHORIZATION_TOKEN_PREFIX.Length..];
                }
            }
            return null;
        }
    }
}
