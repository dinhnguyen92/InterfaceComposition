using System.Net;
using InterfaceComposition.REST.UrlParameters;
using InterfaceComposition.REST.RestRequests.Builders;
using InterfaceComposition.REST.RestRequests.Definitions;
using System.Collections.Generic;
using System.Net.Http;
using System;
using System.Threading.Tasks;

namespace InterfaceComposition.ApiClients
{
    public class HttpRestClient
    {
        protected string BaseUrl { get; }
        protected IDictionary<string, string>? DefaultRequestHeaders { get; }

        public HttpRestClient(string baseUrl, IDictionary<string, string>? defaultRequestHeaders)
        {
            BaseUrl = baseUrl;
            DefaultRequestHeaders = defaultRequestHeaders;
        }

        protected HttpClient BuildHttpClient()
        {
            HttpClient client = new() { BaseAddress = new Uri(BaseUrl) };
            client.DefaultRequestHeaders.Accept.Clear();

            if (DefaultRequestHeaders != null)
            {
                foreach (var header in DefaultRequestHeaders)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
            return client;
        }


        #region GET/DELETE Methods

        public Task<HttpResponseMessage> GetAsync(string relativePath, UrlParameterList? urlParameters = null)
        {
            var requestBuilder = new NonPayloadRestRequestBuilder(HttpMethod.Get, relativePath, urlParameters);
            return SendNonPayloadRequestWithRetryAsync(requestBuilder.Build());
        }

        public Task<HttpResponseMessage> DeleteAsync(string relativePath, UrlParameterList? urlParameters = null)
        {
            var requestBuilder = new NonPayloadRestRequestBuilder(HttpMethod.Delete, relativePath, urlParameters);
            return SendNonPayloadRequestWithRetryAsync(requestBuilder.Build());
        }

        #endregion


        #region POST/PUT/PATCH Methods

        public Task<HttpResponseMessage> PostAsync(
            BasePayloadRestRequestBuilder requestBuilder,
            string relativePath,
            UrlParameterList? urlParameters = null)
        {
            requestBuilder.SetMethod(HttpMethod.Post);
            return SendPayloadRequestAsync(requestBuilder, relativePath, urlParameters);
        }

        public Task<HttpResponseMessage> PutAsync(
            BasePayloadRestRequestBuilder requestBuilder,
            string relativePath,
            UrlParameterList? urlParameters = null)
        {
            requestBuilder.SetMethod(HttpMethod.Put);
            return SendPayloadRequestAsync(requestBuilder, relativePath, urlParameters);
        }

        public Task<HttpResponseMessage> PatchAsync(
            BasePayloadRestRequestBuilder requestBuilder,
            string relativePath,
            UrlParameterList? urlParameters = null)
        {
            requestBuilder.SetMethod(HttpMethod.Patch);
            return SendPayloadRequestAsync(requestBuilder, relativePath, urlParameters);
        }

        private Task<HttpResponseMessage> SendPayloadRequestAsync(
            BasePayloadRestRequestBuilder requestBuilder,
            string relativePath,
            UrlParameterList? urlParameters = null)
        {
            requestBuilder.SetRelativePath(relativePath);
            if (urlParameters != null)
            {
                requestBuilder.AddUrlParameters(urlParameters);
            }
            return SendPayloadRequestWithRetryAsync(requestBuilder.Build());
        }

        #endregion


        #region Retry Logic

        protected async Task<HttpResponseMessage> SendNonPayloadRequestWithRetryAsync(NonPayloadRestRequest request)
        {
            Func<HttpClient, Task<HttpResponseMessage>> sendRequestAsync = request.MethodName switch
            {
                "GET" => async client => await client.GetAsync(request.RequestUri),
                "DELETE" => async client => await client.DeleteAsync(request.RequestUri),
                _ => throw new Exception($"Unsupported non-payload HTTP method {request.MethodName}")
            };
            return await ExecuteWithRetryAsync(sendRequestAsync);
        }

        protected async Task<HttpResponseMessage> SendPayloadRequestWithRetryAsync(PayloadRestRequest request)
        {
            Func<HttpClient, Task<HttpResponseMessage>> sendRequestAsync = request.MethodName switch
            {
                "POST" => async client => await client.PostAsync(request.RequestUri, request.PayloadContent),
                "PUT" => async client => await client.PutAsync(request.RequestUri, request.PayloadContent),
                "PATCH" => async client => await client.PatchAsync(request.RequestUri, request.PayloadContent),
                _ => throw new Exception($"Unsupported payload HTTP method {request.MethodName}")
            };
            return await ExecuteWithRetryAsync(sendRequestAsync);
        }

        private async Task<HttpResponseMessage> ExecuteWithRetryAsync(Func<HttpClient, Task<HttpResponseMessage>> sendRequestAsync)
        {
            int retries = 0;
            const int maxRetries = 5;
            HttpResponseMessage response;
            TimeSpan delay = TimeSpan.FromMilliseconds(0);

            using (var client = BuildHttpClient())
            {
                do
                {
                    await Task.Delay(delay);
                    response = await sendRequestAsync(client);
                    delay = TimeSpan.FromMilliseconds(Math.Pow(2, retries) * 500);
                    retries++;
                }
                while (response.StatusCode == HttpStatusCode.TooManyRequests && retries < maxRetries);
            }

            return response;
        }

        #endregion


        #region Response Handling

        public async Task<string> ReadMessageAndStatusCodeAsync(HttpResponseMessage response)
        {
            string content = await response.Content.ReadAsStringAsync();
            return $"Status code: {response.StatusCode}. Message: {content}";
        }

        #endregion
    }
}
