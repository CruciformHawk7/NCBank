using System.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NCBank.Models;
using Microsoft.AspNetCore.Http;

namespace NCBank.Pages {
    public class PaymentsModel : PageModel {
        [BindProperty] public Models.Transaction transaction {get; set;}
        [ViewData] public string ToEmailError {get; set; }
        [ViewData] public string TransErr {get; set; }
        public void OnGet() {

        }

        public async Task<IActionResult> OnPostAsync() {
            if (ModelState.IsValid) {
                BankCustomer user = SessionManager.GetSession(HttpContext.Session.GetString("sessionID"));
                if (Models.Transaction.CheckEmail(transaction.ToEmail)) {
                    transaction.FromEmail = user.Email;
                    transaction.Type = TransactionType.NetBanking;
                    bool res = await Models.Transaction.DoTransaction(transaction);
                    if (!res) {
                        TransErr = "You do not have sufficient balance.";
                    } else {
                        TransErr = "Payment Successful.";
                    }
                    return Page();
                } else {
                    ToEmailError = "This user does not exist.";
                    return Page();
                } 
            } else {
                return Page();
            }
        }
    }
}
