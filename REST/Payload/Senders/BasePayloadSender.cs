using InterfaceComposition.ApiClients;
using InterfaceComposition.REST.RestRequests.Builders;
using InterfaceComposition.REST.UrlParameters;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace InterfaceComposition.REST.Payload.Senders
{
    public abstract class BasePayloadSender
    {
        protected HttpRestClient RestClient { get; }

        public BasePayloadSender(HttpRestClient restClient)
        {
            RestClient = restClient;
        }

        protected abstract BasePayloadRestRequestBuilder InitializeRequestBuilderWithPayload<TPayLoad>(TPayLoad payload);

        public Task<HttpResponseMessage> PostAsync<TPayLoad>(
            string relativePath,
            TPayLoad payload,
            UrlParameterList? urlParameters = null) =>
            RestClient.PostAsync(
                InitializeRequestBuilderWithPayload(payload),
                relativePath,
                urlParameters);

        public async Task PostAndHandleResponseAsync<TPayLoad>(
            string relativePath,
            TPayLoad payload,
            UrlParameterList? urlParameters = null)
        {
            var response = await PostAsync(relativePath, payload, urlParameters);
            await HandleResponseAsync<TPayLoad>(response);
        }

        public Task<HttpResponseMessage> PutAsync<TPayLoad>(
            string relativePath,
            TPayLoad payload,
            UrlParameterList? urlParameters = null) =>
            RestClient.PutAsync(
                InitializeRequestBuilderWithPayload(payload),
                relativePath,
                urlParameters);

        public async Task PutAndHandleResponseAsync<TPayLoad>(
            string relativePath,
            TPayLoad payload,
            UrlParameterList? urlParameters = null)
        {
            var response = await PutAsync(relativePath, payload, urlParameters);
            await HandleResponseAsync<TPayLoad>(response);
        }

        public Task<HttpResponseMessage> PatchAsync<TPayLoad>(
            string relativePath,
            TPayLoad payload,
            UrlParameterList? urlParameters = null) =>
            RestClient.PatchAsync(
                InitializeRequestBuilderWithPayload(payload),
                relativePath,
                urlParameters);

        public async Task PatchAndHandleResponseAsync<TPayLoad>(
            string relativePath,
            TPayLoad payload,
            UrlParameterList? urlParameters = null)
        {
            var response = await PatchAsync(relativePath, payload, urlParameters);
            await HandleResponseAsync<TPayLoad>(response);
        }

        protected virtual async Task HandleResponseAsync<TPayLoad>(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await RestClient.ReadMessageAndStatusCodeAsync(response);
                throw new Exception($"Error sending {nameof(TPayLoad)}. {errorMessage}");
            }
        }
    }
}
