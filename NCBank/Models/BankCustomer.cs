using System.Runtime.CompilerServices;
using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NCBank.Models {
    public class BankCustomer {
        [BsonId]
        public ObjectId Id {get; set; }

        [BsonElement("firstName")]
        [Required(ErrorMessage="Enter your First Name")]
        public string FirstName { get; set; }

        [BsonElement("lastName")]
        [Required(ErrorMessage="Enter your Last Name")]
        public string LastName { get; set; }
        
        [BsonElement("email")]
        [NotNull]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid Email Address")]
        [Required(ErrorMessage="Enter an email address")]
        public string Email {get; set; }

        [BsonElement("age")]
        [Range(18, 150, ErrorMessage = "Age out of bounds!")]
        [Required(ErrorMessage = "No Age entered.")]
        public int Age { get; set; }

        [BsonElement("gender")]
        [RegularExpression("M|F|O|male|female|other", ErrorMessage = "Invalid Gender value")]
        public string Gender { get; set; }

        [BsonElement("maritalStatus")]
        [RegularExpression("M|S|O|married|single|other", ErrorMessage = "Invalid Gender value")]
        public string MaritalStatus { get; set; }

        [BsonElement("passwordHash")]
        [NotNull]
        [Required]
        public string Password { get; set; } 
        
        [BsonElement("houseName")]
        [Required]
        [NotNull]
        public string HouseName { get; set; }

        [BsonElement("houseNumber")]
        [Required]
        [NotNull]
        public string HouseNumber { get; set; }

        [BsonElement("firstAddress")]
        [Required]
        [NotNull]
        public string FirstAddress { get; set; }

        [BsonElement("secondAddress")]
        [Required]
        [NotNull]
        public string SecondAddress { get; set; }

        [BsonElement("city")]
        [Required]
        [NotNull]
        public string City { get; set; }

        [BsonElement("state")]
        [Required]
        [NotNull]
        public string State { get; set; }

        [BsonElement("phone")]
        [RegularExpression("^[0-9]{10,15}$", ErrorMessage = "Invalid phone number.")]
        [Required]
        [NotNull]
        public string Phone { get; set; }
        
        [BsonElement("jobTitle")]
        [Required]
        [NotNull]
        public string JobTitle { get; set; }

        [BsonElement("orgName")]
        [Required]
        [NotNull]
        public string OrgName { get; set; }

        [BsonElement("orgCity")]
        [Required]
        [NotNull]
        public string OrgCity { get; set; }

        [BsonElement("orgPhone")]
        [RegularExpression("^[0-9]{10,15}$", ErrorMessage = "Invalid phone number.")]
        [Required]
        [NotNull]
        public string OrgPhone { get; set; }
        
        [BsonElement("aadhar")]
        [RegularExpression("^[0-9]{12}$", ErrorMessage = "Invalid Aadhar number.")]
        [Required]
        [NotNull]
        public string Aadhar { get; set; }

        [BsonElement("pan")]
        [RegularExpression("^[A-Za-z]{5}[0-9]{4}[A-Za-z]{1}$", ErrorMessage = "Invalid PAN")]
        [Required]
        [NotNull]
        public string Pan { get; set; }

        [BsonElement("verified")]
        public bool Verified {get; set; }

        [BsonElement("dateCreated")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local, DateOnly=true)]
        public DateTime DateCreated { get; set; }

        public void Prepare() {
            Password = Hashing.Hash(Password);
            DateCreated = DateTime.Now.Date;
        }

        public static BankCustomer ToBankCustomer(BsonDocument doc) {
            return BsonSerializer.Deserialize<BankCustomer>(doc);
        }

        internal static BankCustomer InnerClone(BankCustomer another) {
            return new BankCustomer() {
                Id = another.Id,
                FirstName = another.FirstName,
                LastName = another.LastName,
                Email = another.Email,
                Age = another.Age,
                Gender = another.Gender,
                MaritalStatus = another.MaritalStatus,
                Password = "",
                HouseName = another.HouseName,
                HouseNumber = another.HouseNumber,
                FirstAddress = another.FirstAddress,
                SecondAddress = another.SecondAddress,
                City = another.City,
                State = another.State,
                Phone = another.Phone,
                JobTitle = another.JobTitle,
                OrgName = another.OrgName,
                OrgCity = another.OrgCity,
                OrgPhone = another.OrgPhone,
                Aadhar = another.Aadhar,
                Pan = another.Pan                
            };
        }

        internal void Update(BankCustomer another) {
            FirstName = another.FirstName;
            LastName = another.LastName;
            Age = another.Age;
            Gender = another.Gender;
            MaritalStatus = another.MaritalStatus;
            HouseName = another.HouseName;
            HouseNumber = another.HouseNumber;
            FirstAddress = another.FirstAddress;
            SecondAddress = another.SecondAddress;
            City = another.City;
            State = another.State;
            Phone = another.Phone;
            JobTitle = another.JobTitle;
            OrgName = another.OrgName;
            OrgCity = another.OrgCity;
            OrgPhone = another.OrgPhone;
            Aadhar = another.Aadhar;
            Pan = another.Pan;
        }

        public async Task<List<Models.Transaction>> GetTransactions() {
            var filterTo = Builders<Transaction>.Filter.Eq("to", this.Email);
            var filterFr = Builders<Transaction>.Filter.Eq("from", this.Email);
            var filterOr = Builders<Transaction>.Filter.Or(filterTo, filterFr);
            var find =  await DBInterface.tran.FindAsync(filterOr);
            return await find.ToListAsync();
        }

        public async Task<CustomerBalance> GetBalanceObj() {
            var filter = Builders<CustomerBalance>.Filter.Eq("email", this.Email);
            return await DBInterface.bal.Find(filter).FirstOrDefaultAsync();
        }
    }
}