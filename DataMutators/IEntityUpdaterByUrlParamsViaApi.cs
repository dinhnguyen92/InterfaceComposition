using InterfaceComposition.DataFilters;
using System.Threading.Tasks;

namespace InterfaceComposition.DataMutators
{
    public interface IEntityUpdaterByUrlParamsViaApi<TUpdatePayload, TEntityUrlParamFilter> :
        IEntityUpdaterViaApi<TUpdatePayload>
        where TUpdatePayload : class
        where TEntityUrlParamFilter : IEntityUrlParamFilter
    {
        public Task UpdateEntityAsync(
            TUpdatePayload updatePayload,
            TEntityUrlParamFilter entityUrlParamFilter) =>
            UpdateEntityInternalAsync(
                Router.EntityUpdateByUrlParamsRoute.UrlEncodedText,
                updatePayload,
                entityUrlParamFilter.ToUrlParameters());
    }
}
