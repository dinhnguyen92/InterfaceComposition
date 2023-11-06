using InterfaceComposition.DataFilters;
using System.Threading.Tasks;

namespace InterfaceComposition.DataMutators
{
    public interface IEntityDeleterByUrlParamsViaApi<TEntityUrlParamFilter> : IEntityDeleterViaApi
        where TEntityUrlParamFilter : IEntityUrlParamFilter
    {
        public Task DeleteEntityAsync(TEntityUrlParamFilter entityUrlParamFilter) =>
            DeleteEntityInternalAsync(
                Router.EntityDeletionByUrlParamsRoute.UrlEncodedText,
                entityUrlParamFilter.ToUrlParameters());
    }
}
