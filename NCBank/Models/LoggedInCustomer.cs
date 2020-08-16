using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NCBank.Models {
    public class LoggedInCustomer {
        private string _password;

        [BsonId]
        public ObjectId Id {get; set; }
        public string Email {get; set; }
        public string Password {
            get => _password;
            set => _password = Hashing.Hash(value);
        }

        public bool VerifyPassword(string password) {
            return password==Password;
        }

    }
}