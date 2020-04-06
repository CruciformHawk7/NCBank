using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NCBank.Models;

namespace NCBank.Pages
{
    public class AccountModel : PageModel
    {
        [BindProperty]
        BankCustomer customer { get; set; }
        public void OnGet()
        {
        }
    }
}
