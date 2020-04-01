using MongoDB.Bson;
using MongoDB.Driver;

namespace NCBank.Models {
    static class DBInterface {
        static IMongoDatabase db = new MongoClient().GetDatabase("bankNC");
        static internal IMongoCollection<BsonDocument> cust = db.GetCollection<BsonDocument>("customers");

        static public void InsertCustomer(BsonDocument doc) {
            cust.InsertOne(doc);
        }
    }
}