using System;
using System.ComponentModel.DataAnnotations;

namespace NCBank.Models {
    public class BankCustomer {
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Account Creation Time Error")]
        public DateTime AccountCreation { get; set; }
        
    }
}