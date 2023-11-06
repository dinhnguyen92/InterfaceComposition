using System.Threading.Tasks;

namespace InterfaceComposition.DataMutators
{
    public interface IEntityUpdaterByIdViaApi<TEntityId, TUpdatePayload> :
        IEntityUpdaterViaApi<TUpdatePayload>
        where TEntityId : notnull
        where TUpdatePayload : class
    {
        public Task UpdateEntityAsync(
            TEntityId entityId,
            TUpdatePayload updatePayload) =>
            UpdateEntityInternalAsync(
                Router.EntityUpdateByIdRoute(entityId).UrlEncodedText,
                updatePayload);
    }
}
