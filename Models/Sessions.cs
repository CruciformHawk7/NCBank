using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NCBank.Models {
    public class Sessions {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("sessionID")]
        public string SessionID { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }
    }
}