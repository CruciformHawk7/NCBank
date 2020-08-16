using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using NCBank.Models;

namespace NCTests {
    [TestClass]
    public class DatabaseInsertTests {
        private List<BankCustomer> _mockCustomers;
        
        [TestInitialize]
        [TestMethod]
        public void InsertCustomers() {
            _mockCustomers = new List<BankCustomer>();
            for (int i = 0; i < 3; i++) {
                _mockCustomers.Add(new MockUserGenerator().GetMockUser());
            }
            try {
                foreach (var mock in _mockCustomers) {
                    DBInterface.cust.InsertOne(mock);
                }
            } catch (Exception) {
                Assert.Fail("Insertion failed.");
            }
        }

        [TestMethod]
        public void FindInsertedCustomers() {
            foreach (var mock in _mockCustomers) {
                var filter = Builders<BankCustomer>.Filter.Eq("email", mock.Email);
                var project = Builders<BankCustomer>.Projection
                    .Include("email")
                    .Include("firstName")
                    .Include("lastName");
                var doc = DBInterface.cust.Find(filter).Project(project).FirstOrDefault();
                Assert.AreEqual(doc["firstName"], mock.FirstName, "Names didnt match.");
            }
        }

        [TestMethod]
        [TestCleanup]
        public void RemoveInsertedCustomers() {
            foreach (var mock in _mockCustomers) {
                var filter = Builders<BankCustomer>.Filter.Eq("email", mock.Email);
                var result = DBInterface.cust.DeleteOne(filter);
                Assert.IsTrue(result.IsAcknowledged, $"Delete failed for {mock.FirstName}.");
            }
        }
        
    }
}