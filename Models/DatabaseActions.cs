using MongoDB.Bson;
using MongoDB.Driver;

namespace NCBank.Models {
    static class DBInterface {
        static IMongoDatabase db = new MongoClient().GetDatabase("bankNC");
        static internal IMongoCollection<BsonDocument> cust = db.GetCollection<BsonDocument>("customers");
        static internal IMongoCollection<BsonDocument> sess = db.GetCollection<BsonDocument>("sessions");
        static internal IMongoCollection<BsonDocument> bal = db.GetCollection<BsonDocument>("balance");

        static public void InsertCustomer(BsonDocument doc) {
            cust.InsertOne(doc);
        }
    }
}