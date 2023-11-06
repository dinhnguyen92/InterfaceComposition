using InterfaceComposition.DataFilters;
using InterfaceComposition.REST.ApiRouting;
using InterfaceComposition.REST.Paging;
using InterfaceComposition.REST.Payload.Receivers;
using InterfaceComposition.REST.UrlParameters;

namespace InterfaceComposition.DataQueriers
{
    public interface IEntityListerByUrlParamsViaApi<TEntity, TEntityUrlParamFilter>
        where TEntity : class
        where TEntityUrlParamFilter : IEntityUrlParamFilter
    {
        protected BasePayloadReceiver<PagedList<TEntity>> PayloadReceiver { get; }
        protected EntityApiRouter Router { get; }

        public Task<PagedList<TEntity>> GetEntityPagedListAsync(
            PagingParameters pagingParams,
            TEntityUrlParamFilter? entityUrlParamFilter)
        {
            var allParameters = pagingParams.Concat(entityUrlParamFilter?.ToUrlParameters() ?? UrlParameterList.Empty);
            return PayloadReceiver.GetAsync(Router.EntityListRoute.UrlEncodedText, allParameters);
        }
    }
}
