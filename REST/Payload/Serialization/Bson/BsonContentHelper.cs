using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using System.Net.Http.Headers;

namespace InterfaceComposition.REST.Payload.Serialization.Bson
{
    public static class BsonContentHelper
    {
        public static ByteArrayContent SerializeToBsonByteArray<TPayload>(TPayload payload)
        {
            // Serialize the payload into a BsonDocument if it's not already a BsonDocument
            BsonDocument bsonDocument = payload as BsonDocument ?? payload.ToBsonDocument();
            return SerializeBsonDocumentToByteArray(bsonDocument);
        }

        public static ByteArrayContent SerializeBsonDocumentToByteArray(BsonDocument bsonDocument)
        {
            var bsonBytes = bsonDocument.ToBson();
            var content = new ByteArrayContent(bsonBytes);
            content.Headers.ContentType = new MediaTypeHeaderValue(RESTConstants.BSON_MEDIA_TYPE);
            return content;
        }

        public static BsonDocument DeserializeStreamToBsonDocument(Stream stream)
        {
            using (var reader = new BsonBinaryReader(stream))
            {
                var context = BsonDeserializationContext.CreateRoot(reader);
                return BsonDocumentSerializer.Instance.Deserialize(context);
            }
        }

        public static async Task<BsonDocument> DeserializeHttpContentToBsonDocumentAsync(HttpContent httpContent)
        {
            using (var stream = await httpContent.ReadAsStreamAsync())
            {
                return DeserializeStreamToBsonDocument(stream);
            }
        }

        public static T DeserializeStreamAsBsonDocument<T>(Stream stream)
        {
            var bsonDocument = DeserializeStreamToBsonDocument(stream);
            return BsonSerializer.Deserialize<T>(bsonDocument);
        }

        public static async Task<T> DeserializeHttpContentAsBsonDocumentAsync<T>(HttpContent httpContent)
        {
            using (var stream = await httpContent.ReadAsStreamAsync())
            {
                return DeserializeStreamAsBsonDocument<T>(stream);
            }
        }
    }
}
