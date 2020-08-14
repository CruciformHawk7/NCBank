using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NCBank.Models;

namespace NCTests {
    
    /// <summary>
    /// This class contains tests for checking the validity of
    /// <see cref="NCBank.Models.BankCustomer"/> under various
    /// conditions. 
    /// </summary>
    [TestClass]
    public class BankCustomerValidations {

        private TestContext _testContext;
        
        [TestMethod]
        public void AllowValidModel() {
            var bankCustomer = GenerateBankCustomer();
            Assert.IsTrue(ValidateModel(bankCustomer),
                "Valid model rejected!");
        }
        
        
        [TestMethod]
        public void RejectBlankFName() {
            var bankCustomer = GenerateBankCustomer();
            bankCustomer.FirstName = "";
            Assert.IsFalse(ValidateModel(bankCustomer),
                "Model was Valid!");
        }

        [TestMethod]
        public void RejectBlankLName() {
            var bankCustomer = GenerateBankCustomer();
            bankCustomer.LastName = "";
            Assert.IsFalse(ValidateModel(bankCustomer),
                "Model was Valid!");
        }
        
        [TestMethod]
        public void RejectUnderAged() {
            var bankCustomer = GenerateBankCustomer();
            bankCustomer.Age = 8;
            Assert.IsFalse(ValidateModel(bankCustomer),
                "Model was Valid!");
        }
        
        [TestMethod]
        public void RejectOverAged() {
            var bankCustomer = GenerateBankCustomer();
            bankCustomer.Age = 160;
            Assert.IsFalse(ValidateModel(bankCustomer),
                "Model was Valid!");
        }
        
        [TestMethod]
        public void RejectBlankEmail() {
            var bankCustomer = GenerateBankCustomer();
            bankCustomer.Email = "";
            Assert.IsFalse(ValidateModel(bankCustomer),
                "Model was Valid!");
        }
        
        [TestMethod]
        public void RejectInvalidEmail() {
            var bankCustomer = GenerateBankCustomer();
            bankCustomer.Email = "johndoe@johnindustry";
            Assert.IsFalse(ValidateModel(bankCustomer),
                "Model was Valid!");
        }
        
        [TestMethod]
        public void RejectGender() {
            var bankCustomer = GenerateBankCustomer();
            bankCustomer.Gender = "man";
            Assert.IsFalse(ValidateModel(bankCustomer),
                "Model was Valid!");
        }
        
        [TestMethod]
        public void RejectInvalidMaritalStatus() {
            var bankCustomer = GenerateBankCustomer();
            bankCustomer.MaritalStatus = "unmarried";
            Assert.IsFalse(ValidateModel(bankCustomer),
                "Model was Valid!");
        }
        
        [TestMethod]
        public void RejectInvalidAadhar() {
            var bankCustomer = GenerateBankCustomer();
            bankCustomer.Aadhar = "06786";
            Assert.IsFalse(ValidateModel(bankCustomer),
                "Model was Valid!");
        }
        
        [TestMethod]
        public void RejectInvalidPan() {
            var bankCustomer = GenerateBankCustomer();
            bankCustomer.Pan = "3JR4";
            Assert.IsFalse(ValidateModel(bankCustomer),
                "Model was Valid!");
        }
        
        [TestMethod]
        public void RejectAlphanumericPhone() {
            var bankCustomer = GenerateBankCustomer();
            bankCustomer.Phone = "9R2J1K4T5R";
            Assert.IsFalse(ValidateModel(bankCustomer),
                "Model was Valid!");
        }

        [TestMethod]
        public void RejectShortPhone() {
            var bankCustomer = GenerateBankCustomer();
            bankCustomer.Phone = "9882";
            Assert.IsFalse(ValidateModel(bankCustomer),
                "Model was Valid!");
        }
        
        [TestMethod]
        public void RejectLongPhone() {
            var bankCustomer = GenerateBankCustomer();
            bankCustomer.Phone = "12346795799521972197";
            Assert.IsFalse(ValidateModel(bankCustomer),
                "Model was Valid!");
        }
        
        private static BankCustomer GenerateBankCustomer() {
            return new BankCustomer {
                FirstName = "John", LastName = "Doe",
                Aadhar = "789618648935", Age = 33,
                City = "Bengaluru", FirstAddress = "Address",
                DateCreated = DateTime.Now,
                Email = "johndoe@johnindustries.com",
                Gender = "male", HouseName = "Johns Palace",
                HouseNumber = "4556", JobTitle = "Manager",
                MaritalStatus = "married", 
                OrgCity = "Bengaluru",
                OrgName = "John Industries", 
                OrgPhone = "08045661234",
                Phone = "9775299438", Pan = "CARDS9324J",
                Password = "5zss345353", State = "Karnataka",
                SecondAddress = "Main Road", Verified = true
            };
        }

        public TestContext TestContext {
            get => _testContext;
            set => _testContext = value;
        }
        
        private bool ValidateModel(BankCustomer bankCustomer) {
            var context = new ValidationContext(bankCustomer, 
                null, null);
            var results = new List<ValidationResult>();
            var state = Validator.TryValidateObject(bankCustomer, 
                context, results, true);
            if (!state)
                foreach (var result in results) 
                    TestContext.WriteLine(result.ErrorMessage);
            return state;
        }
    }
}