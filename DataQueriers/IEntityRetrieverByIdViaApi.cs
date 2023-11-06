using InterfaceComposition.DomainEntities.Core;
using System.Threading.Tasks;

namespace InterfaceComposition.DataQueriers
{
    public interface IEntityRetrieverByIdViaApi<TEntity, TEntityId> :
        IEntityRetrieverViaApi<TEntity>
        where TEntity : BaseEntityWithId<TEntityId>
        where TEntityId : notnull
    {
        public Task<TEntity> RetrieveEntityAsync(TEntityId entityId) =>
            RetrieveEntityInternalAsync(Router.EntityWithIdRoute(entityId).UrlEncodedText);
    }
}
