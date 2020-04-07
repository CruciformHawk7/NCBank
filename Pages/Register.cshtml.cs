using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using NCBank.Models;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using Newtonsoft.Json;

namespace NCBank.Pages {
    public class Register : PageModel {
        [BindProperty]
        public BankCustomer customer {get; set; }
        public string OTP {get; set;}
        [BindProperty]
        public string EnteredOTP {get; set;}
        
        public async Task<IActionResult> OnPostAsync() {
            if (EnteredOTP == OTP) {
                DBInterface.cust.InsertOne(customer.ToBsonDocument());
                var sess = await SessionManager.InsertSession(customer);
                CustomerBalance balance = new CustomerBalance();
                balance.Email = customer.Email;
                balance.Balance = 0;
                DBInterface.bal.InsertOne(balance.ToBsonDocument());
                HttpContext.Session.SetString("sessionID", sess.SessionID);
                return Redirect("/Dashboard");
            } else return Page();
        }

        private class TemplateData {
            [JsonProperty("username")]
            public string Username {get; set; }
            [JsonProperty("otp")]
            public string OTP {get; set; }
        }

        async public Task Execute() {
            var apiKey = Secrets.APIKEY;
            var client = new SendGridClient(apiKey);

            var gridmess = new SendGridMessage() {
                From = new EmailAddress(Secrets.Email, "NC Bank"), 
                TemplateId = "d-2a2c6351ec3042c3ac363ce900bc9c33"
            };
            gridmess.AddTo(customer.Email);
            gridmess.SetTemplateData(new TemplateData() {
                Username = customer.FirstName + " " + customer.LastName,
                OTP = getOtp()
            });
            var response = await client.SendEmailAsync(gridmess);
            Console.WriteLine("Done!");
        }

        public string getOtp() {
            OTP = SessionManager.GetRandomString(4);
            return OTP;
        }
    }
}