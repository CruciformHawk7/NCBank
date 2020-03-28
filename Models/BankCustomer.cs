using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace NCBank.Models {
    public class BankCustomer {
        private string _Password;
        [Required(ErrorMessage="Enter your First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage="Enter your Last Name")]
        public string LastName { get; set; }
        public string Email {get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string Password { 
            get { return _Password; } 
            set {
                _Password = Hash(value);
            } 
        } 
        
        public string HouseName { get; set; }
        public string HouseNumber { get; set; }
        public string FirstAddress { get; set; }
        public string SecondAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Phone { get; set; }
        
        public string JobTitle { get; set; }
        public string OrgName { get; set; }
        public string OrgCity { get; set; }
        public string OrgPhone { get; set; }
        
        public string Aadhar { get; set; }
        public string Pan { get; set; }


        private string Hash(string password) {
            string op = "";
            using (MD5 md5Hash = MD5.Create()){
                op = GetMd5Hash(md5Hash, password);
            }
            return op;
        }

        static private string GetMd5Hash(MD5 md5Hash, string input) {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));
            return sBuilder.ToString();
        }
    }
}