using MongoDB.Bson;
using MongoDB.Driver;

namespace NCBank.Models {
    public static class DBInterface {
        public static string Database = "bankNC";
        public static IMongoDatabase db = new MongoClient().GetDatabase(Database);
        public static IMongoCollection<BankCustomer> cust = db.GetCollection<BankCustomer>("customers");
        public static IMongoCollection<Sessions> sess = db.GetCollection<Sessions>("sessions");
        public static IMongoCollection<CustomerBalance> bal = db.GetCollection<CustomerBalance>("balance");
        public static IMongoCollection<Transaction> tran = db.GetCollection<Transaction>("transactions");
    }
}