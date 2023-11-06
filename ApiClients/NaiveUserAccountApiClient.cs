using InterfaceComposition.DomainEntities.UserManagement;
using InterfaceComposition.REST.Paging;
using InterfaceComposition.REST.UrlParameters;

namespace InterfaceComposition.ApiClients
{
    public class NaiveUserAccountApiClient : BaseNaiveEntityApiClient<UserAccount>
    {
        public NaiveUserAccountApiClient(
            string apiBaseUrl,
            IDictionary<string, string>? defaultRequestHeaders) :
            base(apiBaseUrl, defaultRequestHeaders)
        {
        }

        public override Task DeleteEntityAsync(string relativePath) =>
            throw new NotImplementedException("Cannot delete user accounts");

        public override Task<PagedList<UserAccount>> ListEntitiesAsync(
            string relativePath,
            PagingParameters pagingParameters,
            UrlParameterList? filterParameters) =>
            throw new NotImplementedException("Cannot retrieve multiple user accounts");
    }
}
