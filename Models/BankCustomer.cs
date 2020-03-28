using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System;
using MongoDB.Bson;

namespace NCBank.Models {
    public class BankCustomer {
        private string _Password;
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
            set { _Password = Hash(value); } 
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


        private string Hash(string password) {
            string op = "";
            using (MD5 md5Hash = MD5.Create()){
                op = GetMd5Hash(md5Hash, password);
            }
            return op;
        }

        public bool verifyPassword(string password) {
            bool op = false;
            using (MD5 md5hash = MD5.Create()) {
                op = VerifyMd5Hash(md5hash, this.Password, password);
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

        static private bool VerifyMd5Hash(MD5 md5Hash, string input, string hash) {
            string hashOfInput = GetMd5Hash(md5Hash, input);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(hashOfInput, hash))
                return true;
            else
                return false;
        }

        public BsonDocument GetBson() {
            BsonDocument doc = new BsonDocument();
            doc.Add(new BsonElement("FirstName", FirstName));
            doc.Add(new BsonElement("LastName", LastName));
            doc.Add(new BsonElement("Email", Email));
            doc.Add(new BsonElement("Age", Age));
            doc.Add(new BsonElement("Gender", Gender));
            doc.Add(new BsonElement("MaritalStatus", MaritalStatus));
            doc.Add(new BsonElement("PasswordHash", Password));

            doc.Add(new BsonElement("HouseName", HouseName));
            doc.Add(new BsonElement("HouseNumber", HouseNumber));
            doc.Add(new BsonElement("FirstAddress", FirstAddress));
            doc.Add(new BsonElement("SecondAddress", SecondAddress));
            doc.Add(new BsonElement("City", City));
            doc.Add(new BsonElement("State", State));
            doc.Add(new BsonElement("Phone", Phone));

            doc.Add(new BsonElement("JobTitle", JobTitle));
            doc.Add(new BsonElement("OrgName", OrgName));
            doc.Add(new BsonElement("OrgCity", OrgCity));
            doc.Add(new BsonElement("OrgPhone", OrgPhone));

            doc.Add(new BsonElement("Aadhar", Aadhar));
            doc.Add(new BsonElement("Pan", Pan));

            return doc;
        }
    }
}