using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using NCBank.Models;
using System.Threading.Tasks;

namespace NCBank.Pages {
    public class Register : PageModel {
        [BindProperty]
        public BankCustomer customer {get; set; }
        
        public async Task<IActionResult> OnPostAsync() {
            DBInterface.cust.InsertOne(customer.ToBsonDocument());
            var sess = await SessionManager.InsertSession(customer);
            CustomerBalance balance = new CustomerBalance();
            balance.Email = customer.Email;
            balance.Balance = 0;

            DBInterface.bal.InsertOne(balance.ToBsonDocument());

            HttpContext.Session.SetString("sessionID", sess.SessionID);

            return Redirect("/Dashboard");
        }
    }
}