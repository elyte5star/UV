using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebAPI.Data.Messaging
{
    public class MongoMQTTData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Topic { get; set; }
        public string Payload { get; set; }
        public DateTime MessageDate { get; set; }

        public string ClientId { get; set; }
    }
}