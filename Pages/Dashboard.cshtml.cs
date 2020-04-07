using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace NCBank.Pages {
    public class Dashboard : PageModel {
        [BindProperty]
        public string User {get; set; }
        public void OnGet() {
            if (HttpContext.Session.GetString("sessionID")==null || HttpContext.Session.GetString("sessioID")!="") {
                Response.Redirect("Index");
            }
        }
    }
}