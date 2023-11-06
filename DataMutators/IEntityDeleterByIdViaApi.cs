using System.Threading.Tasks;

namespace InterfaceComposition.DataMutators
{
    public interface IEntityDeleterByIdViaApi<TEntityId> :
        IEntityDeleterViaApi
        where TEntityId : notnull
    {
        public Task DeleteEntityAsync(TEntityId entityId) =>
            DeleteEntityInternalAsync(
                Router.EntityDeletionByIdRoute(entityId).UrlEncodedText);
    }
}
