using InterfaceComposition.ApiClients;
using InterfaceComposition.REST.ApiRouting;
using InterfaceComposition.REST.UrlParameters;

namespace InterfaceComposition.DataMutators
{
    public interface IEntityDeleterViaApi
    {
        protected HttpRestClient RestClient { get; }
        protected EntityApiRouter Router { get; }

        protected async Task DeleteEntityInternalAsync(
            string relativePath,
            UrlParameterList? urlParameters = null)
        {
            var response = await RestClient.DeleteAsync(relativePath, urlParameters);
            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await RestClient.ReadMessageAndStatusCodeAsync(response);
                throw new Exception($"Error deleting entity. {errorMessage}");
            }
        }
    }
}
