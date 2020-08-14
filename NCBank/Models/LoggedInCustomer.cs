using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NCBank.Models {
    public class LoggedInCustomer {
        private string _Password;

        [BsonId]
        public ObjectId Id {get; set; }
        public string Email {get; set; }
        public string Password {
            get { return _Password; }
            set { _Password = Hashing.Hash(value); }
        }

        public bool verifyPassword(string password) {
            return password==this.Password;
        }

    }
}