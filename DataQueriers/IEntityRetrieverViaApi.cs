using InterfaceComposition.REST.UrlParameters;
using InterfaceComposition.REST.Payload.Retrievers;
using InterfaceComposition.REST.ApiRouting;
using System.Threading.Tasks;

namespace InterfaceComposition.DataQueriers
{
    public interface IEntityRetrieverViaApi<TEntity> where TEntity : class
    {
        protected BasePayloadRetriever<TEntity> PayloadRetriever { get; }
        protected EntityApiRouter Router { get; }

        protected Task<TEntity> RetrieveEntityInternalAsync(
            string relativePath,
            UrlParameterList? urlParameters = null) =>
            PayloadRetriever.GetAsync(
                relativePath,
                urlParameters);
    }
}
