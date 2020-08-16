using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using NCBank.Models;

namespace NCTests {
    [TestClass]
    public class TestTransactions {
        private BankCustomer _customer1, _customer2;
        private double balance1, balance2;
        private int order = 0;

        [TestMethod]
        [TestInitialize]
        public void TestSetup() {
            while (order == 0) {
                // Create new mock users.
                _customer1 = new MockUserGenerator().GetMockUser();
                _customer2 = new MockUserGenerator().GetMockUser();

                // Insert the customers into the database
                DBInterface.cust.InsertOne(_customer1);
                DBInterface.cust.InsertOne(_customer2);

                // Let user1 have a balance between 30,000 and 50,000
                // and user2 have between 2,000 and 3,000
                balance1 = new Random().Next(30000, 50000);
                balance2 = new Random().Next(2000, 3000);

                // Create new object and insert into the database
                var user1 = new CustomerBalance {
                    Email = _customer1.Email,
                    Balance = balance1
                };
                var user2 = new CustomerBalance {
                    Email = _customer2.Email,
                    Balance = balance2
                };
                DBInterface.bal.InsertOne(user1);
                DBInterface.bal.InsertOne(user2);
                order++;
            }
        }

        [TestMethod]
        public void TrySuccessTransaction() {
            while (order == 1) {
                Transaction.DoTransaction(_customer1.Email,
                        _customer2.Email, 25000, "Test Transaction", TransactionType.Transfer)
                    .Wait();
                order++;
            }
        }

        [TestMethod]
        public void TestBalanceDeductionAfterSuccess() {
            while (order == 2) {
                var filter1 = Builders<CustomerBalance>.Filter.Eq("email", _customer1.Email);
                var user1 = DBInterface.bal.Find(filter1).FirstOrDefault();
                Assert.AreEqual(user1.Balance, balance1 - 25000,
                    $"{user1.Balance} != {(balance1)}");
                order++;
            }

        }

        [TestMethod]
        public void TestBalanceAdditionAfterSuccess() {
            while (order == 3) {
                var filter = Builders<CustomerBalance>.Filter.Eq("email", _customer2.Email);
                var user = DBInterface.bal.Find(filter).FirstOrDefault();
                Assert.AreEqual(user.Balance, (balance2 + 25000),
                    $"{user.Balance} != {balance2}");
                order++;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException), "Exception didn't occur")]
        public void TryOverdrawTransaction() {
            Transaction.DoTransaction(_customer2.Email,
                _customer1.Email, 50000, "Test Fail Transaction", TransactionType.NetBanking)
                .Wait();
        }
        
    }
}