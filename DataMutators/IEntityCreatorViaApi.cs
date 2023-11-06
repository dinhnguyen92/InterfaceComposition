using InterfaceComposition.REST.ApiRouting;
using InterfaceComposition.REST.Payload.Senders;

namespace InterfaceComposition.DataMutators
{
    public interface IEntityCreatorViaApi<TEntity> where TEntity : class
    {
        protected BasePayloadSender PayloadSender { get; }
        protected EntityApiRouter Router { get; }

        public Task CreateEntityAsync(TEntity prototype) =>
            PayloadSender.PostAndHandleResponseAsync(
                Router.EntityCreationRoute.UrlEncodedText,
                prototype);
    }
}
