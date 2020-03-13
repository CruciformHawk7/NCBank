using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using MongoDB.Driver;

namespace NCBank.Pages {
    public class Register : PageModel {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        
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
        
        public void OnPost() {
            var db = new MongoClient().GetDatabase("NCBank");
            var customer = db.GetCollection<BsonDocument>("Customer");
            BsonDocument doc = new BSonDocument();
            
            
            Response.Redirect("/Index");
        }
    }
}