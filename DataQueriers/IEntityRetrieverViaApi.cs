using InterfaceComposition.REST.UrlParameters;
using InterfaceComposition.REST.Payload.Receivers;
using InterfaceComposition.REST.ApiRouting;
using System.Threading.Tasks;

namespace InterfaceComposition.DataQueriers
{
    public interface IEntityRetrieverViaApi<TEntity> where TEntity : class
    {
        protected BasePayloadReceiver<TEntity> PayloadReceiver { get; }
        protected EntityApiRouter Router { get; }

        protected Task<TEntity> RetrieveEntityInternalAsync(
            string relativePath,
            UrlParameterList? urlParameters = null) =>
            PayloadReceiver.GetAsync(
                relativePath,
                urlParameters);
    }
}
