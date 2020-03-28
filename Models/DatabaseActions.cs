using MongoDB.Bson;
using MongoDB.Driver;

namespace NCBank.Models {
    static class DBInterface {
        static IMongoDatabase db = new MongoClient().GetDatabase("NCBank");
        static IMongoCollection<BsonDocument> cust = db.GetCollection<BsonDocument>("Customer");

        static public void InsertCustomer(BsonDocument doc) {
            cust.InsertOne(doc);
        }
    }
}