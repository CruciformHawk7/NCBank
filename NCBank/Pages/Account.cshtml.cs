using System.Collections.Immutable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NCBank.Models;
using MongoDB.Driver;
using Microsoft.AspNetCore.Http;

namespace NCBank.Pages {
    public class AccountModel : PageModel {
        [BindProperty]
        public BankCustomer customer { get; set; }
        public void OnGet() {
            var currentData = SessionManager.GetSession(HttpContext.Session.GetString("sessionID"));
            customer = BankCustomer.InnerClone(currentData);
        }

        public void OnPostResetForm() {
            var currentData = SessionManager.GetSession(HttpContext.Session.GetString("sessionID"));
            customer = BankCustomer.InnerClone(currentData);
        }

        public async Task OnPostAsync() {
            var currentData = SessionManager.GetSession(HttpContext.Session.GetString("sessionID"));
            currentData.Update(customer);
            var filter = Builders<BankCustomer>.Filter.Eq("_id", currentData.Id);
            await DBInterface.cust.FindOneAndReplaceAsync(filter, currentData);
        }

        public IActionResult OnPostPassword() {
            return RedirectToPage("Password");
        }
    }
}
