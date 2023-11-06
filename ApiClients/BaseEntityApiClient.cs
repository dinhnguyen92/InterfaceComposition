using InterfaceComposition.REST.ApiRouting;
using InterfaceComposition.REST.ApiRouting.UrlSegments;
using System.Collections.Generic;

namespace InterfaceComposition.ApiClients
{
    public abstract class BaseEntityApiClient
    {
        protected HttpRestClient DefaultRestClient { get; }
        protected EntityApiRouter DefaultEntityApiRouter { get; }

        public BaseEntityApiClient(
            string apiBaseUrl,
            string entityControllerName,
            IDictionary<string, string>? defaultRequestHeaders)
        {
            DefaultRestClient = new HttpRestClient(apiBaseUrl, defaultRequestHeaders);
            var controllerRoute = new CompositeUrlSegment(new LiteralUrlSegment(entityControllerName));
            DefaultEntityApiRouter = new(controllerRoute);
        }
    }
}
