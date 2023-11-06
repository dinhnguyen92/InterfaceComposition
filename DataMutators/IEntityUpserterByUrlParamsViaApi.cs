using InterfaceComposition.DataFilters;
using System.Threading.Tasks;

namespace InterfaceComposition.DataMutators
{
    public interface IEntityUpserterByUrlParamsViaApi<TEntity, TEntityUrlParamFilter> :
        IEntityUpserterViaApi<TEntity>
        where TEntity : class
        where TEntityUrlParamFilter : IEntityUrlParamFilter
    {
        public Task UpsertEntityAsync(
            TEntity prototype,
            TEntityUrlParamFilter entityUrlParamFilter) =>
            UpsertEntityInternalAsync(
                Router.EntityUpsertByUrlParamsRoute.UrlEncodedText,
                prototype,
                entityUrlParamFilter.ToUrlParameters());
    }
}
