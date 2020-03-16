using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using MongoDB.Driver;
using NCBank.Models;

namespace NCBank.Pages {
    public class Register : PageModel {
        [BindProperty]
        public BankCustomer customer {get; set; }
        
        public void OnPost() {
            var db = new MongoClient().GetDatabase("NCBank");
            var cust = db.GetCollection<BsonDocument>("Customer");
            BsonDocument doc = new BsonDocument();
            doc.Add(new BsonElement("FirstName", customer.FirstName));
            doc.Add(new BsonElement("LastName", customer.LastName));
            doc.Add(new BsonElement("Email", customer.Email));
            doc.Add(new BsonElement("Age", customer.Age));
            doc.Add(new BsonElement("Gender", customer.Gender));
            doc.Add(new BsonElement("MaritalStatus", customer.MaritalStatus));
            doc.Add(new BsonElement("PasswordHash", customer.Password));

            doc.Add(new BsonElement("HouseName", customer.HouseName));
            doc.Add(new BsonElement("HouseNumber", customer.HouseNumber));
            doc.Add(new BsonElement("FirstAddress", customer.FirstAddress));
            doc.Add(new BsonElement("SecondAddress", customer.SecondAddress));
            doc.Add(new BsonElement("City", customer.City));
            doc.Add(new BsonElement("State", customer.State));
            doc.Add(new BsonElement("Phone", customer.Phone));

            doc.Add(new BsonElement("JobTitle", customer.JobTitle));
            doc.Add(new BsonElement("OrgName", customer.OrgName));
            doc.Add(new BsonElement("OrgCity", customer.OrgCity));
            doc.Add(new BsonElement("OrgPhone", customer.OrgPhone));

            doc.Add(new BsonElement("Aadhar", customer.Aadhar));
            doc.Add(new BsonElement("Pan", customer.Pan));

            cust.InsertOne(doc);
            
            Response.Redirect("/Index");
        }
    }
}