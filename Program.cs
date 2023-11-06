using InterfaceComposition.ApiClients;
using InterfaceComposition.REST;

namespace InterfaceComposition
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string apiBaseUrl = "https://localhost:5001/api/";
            const string userAccountsBaseUrl = apiBaseUrl + "accounts";
            IDictionary<string, string> userAccountDefaultHeader = new Dictionary<string, string>(new[]
            {
                RESTConstants.BuildAuthorizationHeader("FakeApiKey")
            });

            var naiveUserAccountApiClient = new NaiveUserAccountApiClient(userAccountsBaseUrl, userAccountDefaultHeader);

            var compositeUserAccountApiClient = new CompositeUserAccountApiClient(apiBaseUrl, userAccountDefaultHeader);
        }
    }
}