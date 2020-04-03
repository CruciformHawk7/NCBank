using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NCBank.Models {
    public class BankCustomer {
        private string _Password;
        [BsonId]
        public ObjectId Id {get; set; }
        [Required(ErrorMessage="Enter your First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage="Enter your Last Name")]
        public string LastName { get; set; }
        public string Email {get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string Password { 
            get { return _Password; } 
            set { _Password = Hashing.Hash(value); } 
        } 
        
        public string HouseName { get; set; }
        public string HouseNumber { get; set; }
        public string FirstAddress { get; set; }
        public string SecondAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Phone { get; set; }
        
        public string JobTitle { get; set; }
        public string OrgName { get; set; }
        public string OrgCity { get; set; }
        public string OrgPhone { get; set; }
        
        public string Aadhar { get; set; }
        public string Pan { get; set; }

        public BsonDocument GetBson() {
            BsonDocument doc = new BsonDocument();
            doc.Add(new BsonElement("firstName", FirstName));
            doc.Add(new BsonElement("lastName", LastName));
            doc.Add(new BsonElement("email", Email));
            doc.Add(new BsonElement("age", Age));
            doc.Add(new BsonElement("gender", Gender));
            doc.Add(new BsonElement("maritalStatus", MaritalStatus));
            doc.Add(new BsonElement("passwordHash", Password));

            doc.Add(new BsonElement("houseName", HouseName));
            doc.Add(new BsonElement("houseNumber", HouseNumber));
            doc.Add(new BsonElement("firstAddress", FirstAddress));
            doc.Add(new BsonElement("secondAddress", SecondAddress));
            doc.Add(new BsonElement("city", City));
            doc.Add(new BsonElement("state", State));
            doc.Add(new BsonElement("phone", Phone));

            doc.Add(new BsonElement("jobTitle", JobTitle));
            doc.Add(new BsonElement("orgName", OrgName));
            doc.Add(new BsonElement("orgCity", OrgCity));
            doc.Add(new BsonElement("orgPhone", OrgPhone));

            doc.Add(new BsonElement("aadhar", Aadhar));
            doc.Add(new BsonElement("pan", Pan));

            return doc;
        }
    }
}