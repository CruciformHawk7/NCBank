using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using NCBank.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace NCBank.Pages {
    public class Register : PageModel {
        [BindProperty] public BankCustomer customer {get; set; }
        
        public async Task<IActionResult> OnPostAsync() {
            customer.Prepare();
            customer.Verified = false;
            DBInterface.cust.InsertOne(customer);
            var sess = await SessionManager.InsertSession(customer);
            await ExecuteWelcome();
            CustomerBalance balance = new CustomerBalance();
            balance.Email = customer.Email;
            balance.Balance = 0;
            DBInterface.bal.InsertOne(balance);
            HttpContext.Session.SetString("sessionID", sess.SessionID);
            return Redirect("/Dashboard");
        }

        private class template {
            [JsonProperty("username")]
            public string username {get; set; }
        }

        internal async Task ExecuteWelcome() {
            var apiKey = Secrets.APIKEY;
            var client = new SendGridClient(apiKey);

            var gridmess = new SendGridMessage() {
                From = new EmailAddress(Secrets.Email, "NC Bank"), 
                TemplateId = Secrets.tplWelcome
            };
            gridmess.AddTo(customer.Email);
            gridmess.SetTemplateData(new template() {
                username = customer.FirstName + " " + customer.LastName,
            });
            var response = await client.SendEmailAsync(gridmess);
        }
    }
}