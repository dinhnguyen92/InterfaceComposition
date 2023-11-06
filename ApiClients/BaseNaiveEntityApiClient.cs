using InterfaceComposition.REST.Paging;
using InterfaceComposition.REST.Payload.Serialization.Json;
using InterfaceComposition.REST.RestRequests.Builders;
using InterfaceComposition.REST.UrlParameters;

namespace InterfaceComposition.ApiClients
{
    public abstract class BaseNaiveEntityApiClient<TEntity> : HttpRestClient
    {
        protected BaseNaiveEntityApiClient(
            string baseUrl,
            IDictionary<string, string>? defaultRequestHeaders) :
            base(baseUrl, defaultRequestHeaders)
        {
        }

        protected virtual async Task HandleResponseAsync(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await ReadMessageAndStatusCodeAsync(response);
                throw new Exception($"Error sending {nameof(TEntity)}. {errorMessage}");
            }
        }

        public virtual async Task CreateEntityAsync(string relativePath, TEntity entity)
        {
            var requestBuilder = new JsonPayloadRESTRequestBuilder();
            requestBuilder.SetPayload(entity);
            var response = await PostAsync(requestBuilder, relativePath);
            await HandleResponseAsync(response);
        }

        public virtual async Task UpsertEntityAsync(string relativePath, TEntity entity)
        {
            var requestBuilder = new JsonPayloadRESTRequestBuilder();
            requestBuilder.SetPayload(entity);
            var response = await PatchAsync(requestBuilder, relativePath);
            await HandleResponseAsync(response);
        }

        public virtual async Task DeleteEntityAsync(string relativePath)
        {
            var response = await DeleteAsync(relativePath);
            await HandleResponseAsync(response);
        }

        public virtual async Task<TEntity> RetrieveEntityAsync(
            string relativePath,
            UrlParameterList filterParameters)
        {
            var response = await GetAsync(relativePath, filterParameters);
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await ReadMessageAndStatusCodeAsync(response);
                throw new Exception($"Error getting {nameof(TEntity)}. {errorMessage}");
            }
            return await JsonContentHelper.DeserializeHttpContentAsJsonAsync<TEntity>(response.Content);
        }

        public virtual async Task<PagedList<TEntity>> ListEntitiesAsync(
            string relativePath,
            PagingParameters pagingParameters,
            UrlParameterList? filterParameters)
        {
            var allParameters = pagingParameters.Concat(filterParameters ?? UrlParameterList.Empty);
            var response = await GetAsync(relativePath, allParameters);
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await ReadMessageAndStatusCodeAsync(response);
                throw new Exception($"Error getting {nameof(TEntity)}. {errorMessage}");
            }
            return await JsonContentHelper.DeserializeHttpContentAsJsonAsync<PagedList<TEntity>>(response.Content);
        }
    }
}
