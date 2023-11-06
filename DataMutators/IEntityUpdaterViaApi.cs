using System.Threading.Tasks;
using InterfaceComposition.REST.UrlParameters;
using InterfaceComposition.REST.Payload.Senders;
using InterfaceComposition.REST.ApiRouting;

namespace InterfaceComposition.DataMutators
{
    public interface IEntityUpdaterViaApi<TUpdatePayload>
        where TUpdatePayload : class
    {
        protected BasePayloadSender PayloadSender { get; }
        protected EntityApiRouter Router { get; }

        protected Task UpdateEntityInternalAsync(
            string relativePath,
            TUpdatePayload updatePayload,
            UrlParameterList? urlParameters = null) =>
            PayloadSender.PatchAndHandleResponseAsync(
                relativePath,
                updatePayload,
                urlParameters);
    }
}
