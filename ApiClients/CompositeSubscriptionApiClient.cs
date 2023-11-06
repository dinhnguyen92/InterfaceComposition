using InterfaceComposition.DataMutators;
using InterfaceComposition.DataQueriers;
using InterfaceComposition.DomainEntities.UserManagement;
using InterfaceComposition.REST.ApiRouting;
using InterfaceComposition.REST.Paging;
using InterfaceComposition.REST.Payload.Retrievers;
using InterfaceComposition.REST.Payload.Senders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterfaceComposition.ApiClients
{
    public class CompositeSubscriptionApiClient :
        BaseEntityApiClient,
        IEntityCreatorViaApi<Subscription>,
        IEntityListerByUrlParamsViaApi<Subscription, SubscriptionUrlParamFilter>
    {
        private BsonPayloadSender DefaultPayloadSender { get; }
        private BsonPayloadRetriever<PagedList<Subscription>> SubscriptionsReceiver { get; }

        public CompositeSubscriptionApiClient(
            string apiBaseUrl,
            IDictionary<string, string>? defaultRequestHeaders) :
            base(apiBaseUrl, "accountSubscriptions", defaultRequestHeaders)
        {
            DefaultPayloadSender = new BsonPayloadSender(DefaultRestClient);
            SubscriptionsReceiver = new BsonPayloadRetriever<PagedList<Subscription>>(DefaultRestClient);
        }

        BasePayloadSender IEntityCreatorViaApi<Subscription>.PayloadSender => DefaultPayloadSender;
        EntityApiRouter IEntityCreatorViaApi<Subscription>.Router => DefaultEntityApiRouter;
        public Task CreateAccountSubscriptionAsync(Subscription prototype) =>
            ((IEntityCreatorViaApi<Subscription>)this)
            .CreateEntityAsync(prototype);

        EntityApiRouter IEntityListerByUrlParamsViaApi<Subscription, SubscriptionUrlParamFilter>.Router => DefaultEntityApiRouter;
        BasePayloadRetriever<PagedList<Subscription>> IEntityListerByUrlParamsViaApi<Subscription, SubscriptionUrlParamFilter>.PayloadRetriever => SubscriptionsReceiver;
        public Task<PagedList<Subscription>> ListAccountSubscriptions(
            PagingParameters pagingParams,
            SubscriptionUrlParamFilter urlParamFilter) =>
            ((IEntityListerByUrlParamsViaApi<Subscription, SubscriptionUrlParamFilter>)this)
            .GetEntityPagedListAsync(pagingParams, urlParamFilter);
    }
}
