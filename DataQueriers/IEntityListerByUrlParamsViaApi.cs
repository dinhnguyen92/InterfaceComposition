using InterfaceComposition.DataFilters;
using InterfaceComposition.REST.ApiRouting;
using InterfaceComposition.REST.Paging;
using InterfaceComposition.REST.Payload.Retrievers;
using InterfaceComposition.REST.UrlParameters;
using System.Threading.Tasks;

namespace InterfaceComposition.DataQueriers
{
    public interface IEntityListerByUrlParamsViaApi<TEntity, TEntityUrlParamFilter>
        where TEntity : class
        where TEntityUrlParamFilter : IEntityUrlParamFilter
    {
        protected BasePayloadRetriever<PagedList<TEntity>> PayloadRetriever { get; }
        protected EntityApiRouter Router { get; }

        public Task<PagedList<TEntity>> GetEntityPagedListAsync(
            PagingParameters pagingParams,
            TEntityUrlParamFilter? entityUrlParamFilter)
        {
            var allParameters = pagingParams.Concat(entityUrlParamFilter?.ToUrlParameters() ?? UrlParameterList.Empty);
            return PayloadRetriever.GetAsync(Router.EntityListRoute.UrlEncodedText, allParameters);
        }
    }
}
