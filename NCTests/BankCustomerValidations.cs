using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NCBank.Models;

namespace NCTests {
    
    [TestClass]
    public class BankCustomerValidations {
        
        [TestMethod]
        public void AllowValidModel() {
            var bankCustomer = GenerateBankCustomer();
            Assert.IsTrue(ValidateModel(bankCustomer),"Valid model rejected!");
        }
        
        
        [TestMethod]
        public void RejectBlankFNameCustomer() {
            var bankCustomer = GenerateBankCustomer();
            bankCustomer.FirstName = "";
            Assert.IsFalse(ValidateModel(bankCustomer),"Model was Valid!");
        }

        [TestMethod]
        public void RejectBlankLNameCustomer() {
            var bankCustomer = GenerateBankCustomer();
            bankCustomer.LastName = "";
            Assert.IsFalse(ValidateModel(bankCustomer),"Model was Valid!");
        }
        
        [TestMethod]
        public void RejectUnderAgedCustomer() {
            var bankCustomer = GenerateBankCustomer();
            bankCustomer.Age = 8;
            Assert.IsFalse(ValidateModel(bankCustomer),"Model was Valid!");
        }
        
        [TestMethod]
        public void RejectOverAgedCustomer() {
            var bankCustomer = GenerateBankCustomer();
            bankCustomer.Age = 160;
            Assert.IsFalse(ValidateModel(bankCustomer),"Model was Valid!");
        }
        
        [TestMethod]
        public void RejectBlankEmailCustomer() {
            var bankCustomer = GenerateBankCustomer();
            bankCustomer.Email = "";
            Assert.IsFalse(ValidateModel(bankCustomer),"Model was Valid!");
        }
        
        [TestMethod]
        public void RejectInvalidEmailCustomer() {
            var bankCustomer = GenerateBankCustomer();
            bankCustomer.Email = "johndoe@johnindustry";
            Assert.IsFalse(ValidateModel(bankCustomer),"Model was Valid!");
        }
        
        [TestMethod]
        public void RejectGenderCustomer() {
            var bankCustomer = GenerateBankCustomer();
            bankCustomer.Gender = "man";
            Assert.IsFalse(ValidateModel(bankCustomer),"Model was Valid!");
        }
        
        [TestMethod]
        public void RejectInvalidMaritalStatusCustomer() {
            var bankCustomer = GenerateBankCustomer();
            bankCustomer.MaritalStatus = "unmarried";
            Assert.IsFalse(ValidateModel(bankCustomer),"Model was Valid!");
        }

        private static BankCustomer GenerateBankCustomer() {
            return new BankCustomer {
                FirstName = "John", LastName = "Doe",
                Aadhar = "5633145678", Age = 33,
                City = "Bengaluru", DateCreated = DateTime.Now,
                Email = "johndoe@johnindustries.com", FirstAddress = "Address",
                Gender = "male", HouseName = "Johns Palace",
                HouseNumber = "4556", JobTitle = "Manager",
                MaritalStatus = "married", OrgCity = "Bengaluru",
                OrgName = "John Industries", OrgPhone = "08045661234",
                Phone = "9563119806", Pan = "CACDE9324J",
                Password = "5zss345353", State = "Karnataka",
                SecondAddress = "Main Road", Verified = true
            };
        }

        private bool ValidateModel(BankCustomer bankCustomer) {
            var context = new ValidationContext(bankCustomer, null, null);
            var results = new List<ValidationResult>();
            return Validator.TryValidateObject(bankCustomer, context, results, true);
        }
    }
}