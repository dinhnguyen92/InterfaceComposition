using InterfaceComposition.DataFilters;
using System.Threading.Tasks;

namespace InterfaceComposition.DataQueriers
{
    public interface IEntityRetrieverByUrlParamsViaApi<TEntity, TEntityUrlParamFilter> :
        IEntityRetrieverViaApi<TEntity>
        where TEntity : class
        where TEntityUrlParamFilter : IEntityUrlParamFilter
    {
        public Task<TEntity> RetrieveEntityAsync(TEntityUrlParamFilter entityUrlParamFilter) =>
            RetrieveEntityInternalAsync(
                Router.EntityRetrievalByUrlParamsRoute.UrlEncodedText,
                entityUrlParamFilter.ToUrlParameters());
    }
}
