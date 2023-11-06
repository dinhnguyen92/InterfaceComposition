using InterfaceComposition.DataMutators;
using InterfaceComposition.DataQueriers;
using InterfaceComposition.DomainEntities.UserManagement;
using InterfaceComposition.REST.ApiRouting;
using InterfaceComposition.REST.Paging;
using InterfaceComposition.REST.Payload.Receivers;
using InterfaceComposition.REST.Payload.Senders;

namespace InterfaceComposition.ApiClients
{
    public class CompositeSubscriptionApiClient :
        BaseEntityApiClient,
        IEntityCreatorViaApi<Subscription>,
        IEntityListerByUrlParamsViaApi<Subscription, SubscriptionUrlParamFilter>
    {
        private BsonPayloadSender DefaultPayloadSender { get; }
        private BsonPayloadReceiver<PagedList<Subscription>> SubscriptionsReceiver { get; }

        public CompositeSubscriptionApiClient(
            string apiBaseUrl,
            IDictionary<string, string>? defaultRequestHeaders) :
            base(apiBaseUrl, "accountSubscriptions", defaultRequestHeaders)
        {
            DefaultPayloadSender = new BsonPayloadSender(DefaultRestClient);
            SubscriptionsReceiver = new BsonPayloadReceiver<PagedList<Subscription>>(DefaultRestClient);
        }

        BasePayloadSender IEntityCreatorViaApi<Subscription>.PayloadSender => DefaultPayloadSender;
        EntityApiRouter IEntityCreatorViaApi<Subscription>.Router => DefaultEntityApiRouter;
        public Task CreateAccountSubscriptionAsync(Subscription prototype) =>
            ((IEntityCreatorViaApi<Subscription>)this)
            .CreateEntityAsync(prototype);

        EntityApiRouter IEntityListerByUrlParamsViaApi<Subscription, SubscriptionUrlParamFilter>.Router => DefaultEntityApiRouter;
        BasePayloadReceiver<PagedList<Subscription>> IEntityListerByUrlParamsViaApi<Subscription, SubscriptionUrlParamFilter>.PayloadReceiver => SubscriptionsReceiver;
        public Task<PagedList<Subscription>> ListAccountSubscriptions(
            PagingParameters pagingParams,
            SubscriptionUrlParamFilter urlParamFilter) =>
            ((IEntityListerByUrlParamsViaApi<Subscription, SubscriptionUrlParamFilter>)this)
            .GetEntityPagedListAsync(pagingParams, urlParamFilter);
    }
}
