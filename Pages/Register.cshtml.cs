using System;
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
            DBInterface.cust.InsertOne(customer.ToBsonDocument());

            CustomerBalance balance = new CustomerBalance();
            balance.Email = customer.Email;
            balance.Balance = 0;

            DBInterface.cust.InsertOne(balance.ToBsonDocument());
            Response.Redirect("/Index");
        }
    }
}