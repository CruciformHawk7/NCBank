using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using NCBank.Models;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;

namespace NCBank.Pages {
    public class PasswordModel : PageModel {

        [ViewData]
        public string Issues { get; set; }


        [BindProperty]
        [DisplayName("Old Password")]
        public string Password {get; set; }

        [BindProperty]
        [Required(ErrorMessage="Please enter a password")]
        [DisplayName("New Password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})", 
            ErrorMessage="Your Password must include atleast small and Capital Letters, a number and a symbol, and must be atleast 8 characters long.")]
        public string NewPassword {get; set; }

        [BindProperty]
        [Required(ErrorMessage="Confirm your password")]
        [Compare("NewPassword", ErrorMessage="Passwords don't match")]
        [DisplayName("Confirm New Password")]
        public string NewPasswordConf {get; set; }
        public void OnGet() {

        }

        public IActionResult OnPostBack() {
            return RedirectToPage("Dashboard");
        }

        public async Task<IActionResult> OnPostAsync() {
            var _user = SessionManager.GetSession(HttpContext.Session.GetString("sessionID"));
            var _pass = Hashing.Hash(Password);
            if (ModelState.IsValid) {
                if (_user.Password == _pass) {
                    var filter = Builders<BankCustomer>.Filter.Eq("_id", _user.Id);
                    var update = Builders<BankCustomer>.Update.Set("passwordHash", Hashing.Hash(NewPassword));
                    await DBInterface.cust.UpdateOneAsync(filter, update);
                    return RedirectToPage("Dashboard");
                } else {
                    Issues = "Incorrect Password!";
                    return Page();
                }
            } else {
                return Page();
            }
        }
    }
}
