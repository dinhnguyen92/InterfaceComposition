using InterfaceComposition.DomainEntities.Core;
using System.Threading.Tasks;

namespace InterfaceComposition.DataMutators
{
    public interface IEntityUpserterByIdViaApi<TEntity, TEntityId> :
        IEntityUpserterViaApi<TEntity>
        where TEntity : BaseEntityWithId<TEntityId>
        where TEntityId : notnull
    {
        public Task UpsertEntityAsync(
            TEntityId entityId,
            TEntity prototype) =>
            UpsertEntityInternalAsync(
                Router.EntityUpsertByIdRoute(entityId).UrlEncodedText,
                prototype);
    }
}
