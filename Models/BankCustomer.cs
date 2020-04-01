using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NCBank.Models {

    internal static class Hashing {
        internal static string Hash(string password) {
            string op = "";
            using (MD5 md5Hash = MD5.Create()){
                op = GetMd5Hash(md5Hash, password);
            }
            return op;
        }

        static private string GetMd5Hash(MD5 md5Hash, string input) {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));
            return sBuilder.ToString();
        }

        static internal bool VerifyMd5Hash(MD5 md5Hash, string input, string hash) {
            string hashOfInput = GetMd5Hash(md5Hash, input);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(hashOfInput, hash))
                return true;
            else
                return false;
        }
    }

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
            bool op = false;
            using (MD5 md5hash = MD5.Create()) {
                op = Hashing.VerifyMd5Hash(md5hash, this.Password, password);
            }
            return op;
        }

    }
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