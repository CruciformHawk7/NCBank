using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NCBank.Models;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;

namespace NCBank.Pages{
    public class IndexModel : PageModel{
        [BindProperty]
        public LoggedInCustomer cust {get; set; }

        public IActionResult OnGet() {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("sessionID"))) {
                return RedirectToPage("Dashboard");
            } else {
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync() {
            var filter = Builders<BankCustomer>.Filter.Eq("email", cust.Email);
            var projection = Builders<BankCustomer>.Projection.Include("email").Include("passwordHash");
            var doc = await DBInterface.cust.Find(filter).Project(projection).FirstOrDefaultAsync();
            var user = BankCustomer.ToBankCustomer(doc);
            if (cust.Password.Equals(user.Password)) {
                var sess = await SessionManager.InsertSession(user);
                HttpContext.Session.SetString("sessionID", (sess.SessionID));
                return RedirectToPage("/Dashboard");
            }
            return Page();
        }
    }
}
