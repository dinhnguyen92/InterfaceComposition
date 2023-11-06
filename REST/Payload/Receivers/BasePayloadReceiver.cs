using InterfaceComposition.ApiClients;
using InterfaceComposition.REST.UrlParameters;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace InterfaceComposition.REST.Payload.Receivers
{
    public abstract class BasePayloadReceiver<TPayload>
    {
        protected HttpRestClient RestClient { get; }

        public BasePayloadReceiver(HttpRestClient restClient)
        {
            RestClient = restClient;
        }

        protected abstract Task<TPayload> ParseHttContentForPayloadAsync(HttpContent content);

        public async Task<TPayload> GetAsync(string relativePath, UrlParameterList? urlParameters = null)
        {
            var response = await RestClient.GetAsync(relativePath, urlParameters);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await RestClient.ReadMessageAndStatusCodeAsync(response);
                throw new Exception($"Failed to retrieve {nameof(TPayload)}. {errorMessage}");
            }
            return await ParseHttContentForPayloadAsync(response.Content);
        }
    }
}
