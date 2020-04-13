using System.Collections.Immutable;
using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace NCBank.Models {
    public class Transaction {
        [BsonId] public ObjectId Id {get; set; }
        [BsonElement("from")] public string FromEmail {get; set; }
        [BsonElement("to")] public string ToEmail {get; set; }
        [BsonElement("amount")] public double Amount {get; set; }
        [BsonElement("time")] public DateTime Time {get; set;}
        [BsonElement("comment")] public string Comment {get; set; }
        [BsonElement("type")] public TransactionType Type {get; set; }

        public static async Task DoTransaction(string from, string to, double amount, string comment, 
                TransactionType type) {
            Transaction tr = new Transaction() {
                FromEmail = from,
                ToEmail = to,
                Amount = amount,
                Comment = comment,
                Type = type,
                Time = DateTime.Now
            };
            await DBInterface.tran.InsertOneAsync(tr);
            
            var _FromFilter = Builders<CustomerBalance>.Filter.Eq("email", from);
            var _FromUpd = Builders<CustomerBalance>.Update.Inc("balance", -1*amount);
            await DBInterface.bal.FindOneAndUpdateAsync(_FromFilter, _FromUpd);

            var _ToFilter = Builders<CustomerBalance>.Filter.Eq("email", to);
            var _ToUpd = Builders<CustomerBalance>.Update.Inc("balance", amount);
            await DBInterface.bal.FindOneAndUpdateAsync(_ToFilter, _ToUpd);
        }
    }

    public enum TransactionType {
        NetBanking,
        Transfer,
        Withdrawl
    }
}