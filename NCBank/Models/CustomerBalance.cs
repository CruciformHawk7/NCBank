using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NCBank.Models {
    public class CustomerBalance {
        [BsonId]
        public ObjectId id {get; set; }

        [BsonElement("email")]
        public string Email {get; set; }

        [BsonElement("balance")]
        public double Balance {get; set; }
    }
}