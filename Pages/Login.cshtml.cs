using System.Net;
using System.Security.Cryptography.X509Certificates;
using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NCBank.Models;
using Microsoft.AspNetCore.Http;

namespace NCBank.Pages {
    public class LoginModel : PageModel {
        [BindProperty]
        public LoggedInCustomer cust {get; set; }

        public IActionResult OnGet() {
            if (HttpContext.Session.GetString("sessionID")!=null || HttpContext.Session.GetString("sessionID")!="") {
                return RedirectToPage("Dashboard");
            } else {
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync() {
            var filter = Builders<BankCustomer>.Filter.Eq("email", cust.Email);

            var projection = Builders<BankCustomer>.Projection.Include("email").Include("passwordHash");
            // verify email is a valid account
            var user = await DBInterface.cust.Find(filter).Project(projection).SingleOrDefaultAsync();
            // verify password
            if (cust.verifyPassword(user.GetValue("passwordHash").ToString())) {
                HttpContext.Session.Set("UserEmail", System.Text.Encoding.ASCII.GetBytes(user.GetValue("email").ToString()));
                return RedirectToPage("/Dashboard");
            }
            return Page();
        }
    }
}
