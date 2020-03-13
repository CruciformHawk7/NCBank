using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NCBank.Pages {
    public class RegistrationModel : PageModel {
        public void OnGet(){
            var def = 20;

            def  = def+10;
        }
    }
}
