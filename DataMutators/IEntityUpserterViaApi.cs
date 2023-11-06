using System.Threading.Tasks;
using InterfaceComposition.REST.UrlParameters;
using InterfaceComposition.REST.Payload.Senders;
using InterfaceComposition.REST.ApiRouting;

namespace InterfaceComposition.DataMutators
{
    public interface IEntityUpserterViaApi<TEntity> where TEntity : class
    {
        protected BasePayloadSender PayloadSender { get; }
        protected EntityApiRouter Router { get; }

        protected Task UpsertEntityInternalAsync(
            string relativePath,
            TEntity prototype,
            UrlParameterList? urlParameters = null) =>
            PayloadSender.PutAndHandleResponseAsync(
                relativePath,
                prototype,
                urlParameters);
    }
}
