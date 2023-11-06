using InterfaceComposition.DataMutators;
using InterfaceComposition.DataQueriers;
using InterfaceComposition.DomainEntities.UserManagement;
using InterfaceComposition.REST.ApiRouting;
using InterfaceComposition.REST.Payload.Receivers;
using InterfaceComposition.REST.Payload.Senders;
using InterfaceComposition.UserManagement;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterfaceComposition.ApiClients
{
    public class CompositeUserAccountApiClient :
        BaseEntityApiClient,
        IEntityCreatorViaApi<UserAccount>,
        IEntityUpserterByUrlParamsViaApi<UserAccount, UserAccountUrlParamFilter>,
        IEntityDeleterByIdViaApi<Guid>,
        IEntityRetrieverByIdViaApi<UserAccount, Guid>,
        IEntityRetrieverByUrlParamsViaApi<UserAccount, UserAccountUrlParamFilter>
    {
        private JsonPayloadSender DefaultPayloadSender { get; }
        private JsonPayloadReceiver<UserAccount> UserAccountReceiver { get; }

        public CompositeUserAccountApiClient(
            string apiBaseUrl,
            IDictionary<string, string>? defaultRequestHeaders) :
            base(apiBaseUrl, "accounts", defaultRequestHeaders)
        {
            DefaultPayloadSender = new JsonPayloadSender(DefaultRestClient);
            UserAccountReceiver = new JsonPayloadReceiver<UserAccount>(DefaultRestClient);
        }

        BasePayloadSender IEntityCreatorViaApi<UserAccount>.PayloadSender => DefaultPayloadSender;
        EntityApiRouter IEntityCreatorViaApi<UserAccount>.Router => DefaultEntityApiRouter;
        public Task CreateUserAccountAsync(UserAccount prototype) =>
            ((IEntityCreatorViaApi<UserAccount>)this)
            .CreateEntityAsync(prototype);

        BasePayloadSender IEntityUpserterViaApi<UserAccount>.PayloadSender => DefaultPayloadSender;
        EntityApiRouter IEntityUpserterViaApi<UserAccount>.Router => DefaultEntityApiRouter;
        public Task UpsertUserAccountAsync(
            UserAccount prototype,
            UserAccountUrlParamFilter urlParamFilter) =>
            ((IEntityUpserterByUrlParamsViaApi<UserAccount, UserAccountUrlParamFilter>)this)
            .UpsertEntityAsync(prototype, urlParamFilter);

        HttpRestClient IEntityDeleterViaApi.RestClient => DefaultRestClient;
        EntityApiRouter IEntityDeleterViaApi.Router => DefaultEntityApiRouter;
        public Task DeleteUserAccountAsync(Guid id) =>
            ((IEntityDeleterByIdViaApi<Guid>)this)
            .DeleteEntityAsync(id);

        BasePayloadReceiver<UserAccount> IEntityRetrieverViaApi<UserAccount>.PayloadReceiver => UserAccountReceiver;
        EntityApiRouter IEntityRetrieverViaApi<UserAccount>.Router => DefaultEntityApiRouter;
        public Task<UserAccount> RetrieveUserAccountAsync(Guid id) =>
            ((IEntityRetrieverByIdViaApi<UserAccount, Guid>)this)
            .RetrieveEntityAsync(id);
    }
}
