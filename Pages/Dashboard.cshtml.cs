using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using NCBank.Models;

namespace NCBank.Pages {
    public class Dashboard : PageModel {

        [BindProperty]
        public BankCustomer user { get; set; }
        public IActionResult OnGet() {
            string val = HttpContext.Session.GetString("sessionID");
            if (HttpContext.Session.GetString("sessionID")==null || HttpContext.Session.GetString("sessionID")=="") {
                return Redirect("Index");
            }
            else {
                user = SessionManager.GetSession(HttpContext.Session.GetString("sessionID"));
                return Page();
            }
        }

        public IActionResult OnGetLogout() {
            return OnPostLogout();
        }

        public IActionResult OnPostLogout() {
            SessionManager.RemoveSession(HttpContext.Session.GetString("sessionID"));
            HttpContext.Session.SetString("sessionID", "");
            return RedirectToPage("Index");
        }
    }
}