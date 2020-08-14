using System.Net;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using NCBank.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using Newtonsoft.Json;

namespace NCBank.Pages {
    public class VerifyModel : PageModel {
        public BankCustomer user { get; set; }
        [BindProperty] public string userOtp {get; set; }

        public async Task<IActionResult> OnGetAsync() {
            string val = HttpContext.Session.GetString("sessionID");
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("sessionID"))) {
                return RedirectToPage("Index");
            }
            else {
                user = SessionManager.GetSession(HttpContext.Session.GetString("sessionID"));
                await ExecuteOTP();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync() {
            var OTP = HttpContext.Session.GetString("OTP");
            user = SessionManager.GetSession(HttpContext.Session.GetString("sessionID"));
            if (userOtp.Equals(OTP, StringComparison.InvariantCultureIgnoreCase)) {
                var filter = Builders<BankCustomer>.Filter.Eq("_id", user.Id);
                var update = Builders<BankCustomer>.Update.Set("verified", true);
                var result = await DBInterface.cust.UpdateOneAsync(filter, update);
                HttpContext.Session.SetString("OTP", "");
                return RedirectToPage("Dashboard");
            }
            return Page();
        }

        private class TemplateData {
            [JsonProperty("username")] public string Username {get; set; }
            [JsonProperty("otp")] public string OTP {get; set; }
        }

        async public Task ExecuteOTP() {
            var apiKey = Secrets.APIKEY;
            var client = new SendGridClient(apiKey);

            var gridmess = new SendGridMessage() {
                From = new EmailAddress(Secrets.Email, "NC Bank"), 
                TemplateId = Secrets.tplOtp
            };
            gridmess.AddTo(user.Email);
            gridmess.SetTemplateData(new TemplateData() {
                Username = user.FirstName + " " + user.LastName,
                OTP = getOtp()
            });
            var response = await client.SendEmailAsync(gridmess);
            Console.WriteLine(response);
        }

        public string getOtp() {
            var OT = SessionManager.GetRandomString(6);
            HttpContext.Session.SetString("OTP", OT);
            return OT;
        }
    }
}
