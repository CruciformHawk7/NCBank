using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NCBank.Models {
    public class Transaction {
        [BsonId] public ObjectId Id {get; set; }
        [BsonElement("from")] public string FromEmail {get; set; }
        [BsonElement("to")] public string ToEmail {get; set; }
        [BsonElement("amount")] public double Amount {get; set; }
        [BsonElement("comment")] public string Comment {get; set; }
        [BsonElement("type")] public TransactionType Type {get; set; }
    }

    public enum TransactionType {
        NetBanking,
        Transfer,
        Withdrawl
    }
}