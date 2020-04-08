using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization;

namespace NCBank.Models {
    public class BankCustomer {
        private string _Password;
        [BsonId]
        public ObjectId Id {get; set; }

        [BsonElement("firstName")]
        [Required(ErrorMessage="Enter your First Name")]
        public string FirstName { get; set; }

        [BsonElement("lastName")]
        [Required(ErrorMessage="Enter your Last Name")]
        public string LastName { get; set; }
        
        [BsonElement("email")]
        public string Email {get; set; }

        [BsonElement("age")]
        public int Age { get; set; }

        [BsonElement("gender")]
        public string Gender { get; set; }

        [BsonElement("maritalStatus")]
        public string MaritalStatus { get; set; }

        [BsonElement("passwordHash")]
        public string Password { 
            get { return _Password; } 
            set { _Password = Hashing.Hash(value); } 
        } 
        
        [BsonElement("houseName")]
        public string HouseName { get; set; }

        [BsonElement("houseNumber")]
        public string HouseNumber { get; set; }

        [BsonElement("firstAddress")]
        public string FirstAddress { get; set; }

        [BsonElement("secondAddress")]
        public string SecondAddress { get; set; }

        [BsonElement("city")]
        public string City { get; set; }

        [BsonElement("state")]
        public string State { get; set; }

        [BsonElement("phone")]
        public string Phone { get; set; }
        
        [BsonElement("jobTitle")]
        public string JobTitle { get; set; }

        [BsonElement("orgName")]
        public string OrgName { get; set; }

        [BsonElement("orgCity")]
        public string OrgCity { get; set; }

        [BsonElement("orgPhone")]
        public string OrgPhone { get; set; }
        
        [BsonElement("aadhar")]
        public string Aadhar { get; set; }

        [BsonElement("pan")]
        public string Pan { get; set; }

        [BsonElement("verified")]
        public bool Verified {get; set; }

        public static BankCustomer ToBankCustomer(BsonDocument doc) {
            return BsonSerializer.Deserialize<BankCustomer>(doc);
        }
    }
}