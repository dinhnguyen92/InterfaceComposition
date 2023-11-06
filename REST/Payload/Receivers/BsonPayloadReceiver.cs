using InterfaceComposition.ApiClients;
using InterfaceComposition.REST.Payload.Serialization.Bson;
using MongoDB.Bson;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace InterfaceComposition.REST.Payload.Receivers
{
    public class BsonPayloadReceiver<TPayload> :
        BasePayloadReceiver<TPayload>
        where TPayload : class
    {
        public BsonPayloadReceiver(HttpRestClient restClient) : base(restClient)
        {
        }

        protected override async Task<TPayload> ParseHttContentForPayloadAsync(HttpContent content)
        {
            if (typeof(TPayload) == typeof(BsonDocument))
            {
                var bsonDocument = await BsonContentHelper.DeserializeHttpContentToBsonDocumentAsync(content);
                return bsonDocument as TPayload ??
                    // This should not occur if TResult is really BsonDocument
                    throw new Exception($"Cannot cast deserialized BsonDocument to {typeof(TPayload).Name}");
            }
            return await BsonContentHelper.DeserializeHttpContentAsBsonDocumentAsync<TPayload>(content);
        }
    }
}
