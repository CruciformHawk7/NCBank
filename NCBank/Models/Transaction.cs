using System.Linq;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Collections.Immutable;
using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using MongoDB.Driver;

namespace NCBank.Models {
    public class Transaction {
        [BsonId] public ObjectId Id {get; set; }
        [BsonElement("from")] public string FromEmail {get; set; }

        [BsonElement("to")] 
        [Required(ErrorMessage="Specify your sender here.")]
        [DataType(DataType.EmailAddress, ErrorMessage="Invalid Email Address")]
        public string ToEmail {get; set; }


        [BsonElement("amount")] 
        [Required(ErrorMessage="Enter the amount to send")]
        [DataType(DataType.Currency)]
        public double Amount {get; set; }


        [BsonElement("time")] 
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Time {get; private set;}
        
        [BsonElement("comment")] public string Comment {get; set; }
        [BsonElement("type")] public TransactionType Type {get; set; }

        public static async Task DoTransaction(string from, string to, double amount, string comment, 
                TransactionType type) {
            Transaction tr = new Transaction() {
                FromEmail = from,
                ToEmail = to,
                Amount = amount,
                Comment = comment,
                Type = type,
                Time = DateTime.Now
            };
            await DBInterface.tran.InsertOneAsync(tr);
            
            var _FromFilter = Builders<CustomerBalance>.Filter.Eq("email", from);
            var _FromProject = Builders<CustomerBalance>.Projection.Include("balance");
            var _FromTest = DBInterface.bal.Find(_FromFilter).Project(_FromProject).FirstOrDefault();
            if (_FromTest["balance"] < amount) {
                throw new LowBalanceException(from, to);
            }
            var _FromUpd = Builders<CustomerBalance>.Update.Inc("balance", -1*amount);
            await DBInterface.bal.FindOneAndUpdateAsync(_FromFilter, _FromUpd);

            var _ToFilter = Builders<CustomerBalance>.Filter.Eq("email", to);
            var _ToUpd = Builders<CustomerBalance>.Update.Inc("balance", amount);
            await DBInterface.bal.FindOneAndUpdateAsync(_ToFilter, _ToUpd);
        }

        public static async Task<Boolean> DoTransaction(Transaction transaction) {
            transaction.Time = DateTime.Now;
            var result = await Payable(transaction.FromEmail, transaction.Amount);
            if (result) {
                await DBInterface.tran.InsertOneAsync(transaction);
                
                var _FromFilter = Builders<CustomerBalance>.Filter.Eq("email", transaction.FromEmail);
                var _FromUpd = Builders<CustomerBalance>.Update.Inc("balance", -1*transaction.Amount);
                await DBInterface.bal.FindOneAndUpdateAsync(_FromFilter, _FromUpd);

                var _ToFilter = Builders<CustomerBalance>.Filter.Eq("email", transaction.ToEmail);
                var _ToUpd = Builders<CustomerBalance>.Update.Inc("balance", transaction.Amount);
                await DBInterface.bal.FindOneAndUpdateAsync(_ToFilter, _ToUpd);
                return true;
            }
            else return false;
        }

        public static bool CheckEmail(string email) {
            var Filter = Builders<BankCustomer>.Filter.Eq("email", email);
            var list = DBInterface.cust.Find(Filter).ToList();
            if (list.Count == 0) return false;
            else return true;
        }

        private static async Task<Boolean> Payable(string fromEmail, double amount) {
            var filter = Builders<CustomerBalance>.Filter.Eq("email", fromEmail);
            var bc = await DBInterface.bal.FindAsync(filter);
            var cs = await bc.FirstOrDefaultAsync();
            if (cs.Balance > amount) return true;
            else return false;
        }
    }

    public enum TransactionType {
        NetBanking,
        Transfer,
        Withdrawl
    }

    public class LowBalanceException : Exception {
        private readonly string _sender, _receiver;

        public LowBalanceException(string sender, string receiver) {
            _sender = sender;
            _receiver = receiver;
        }
        public override string Message => $"Balance is too low to send from {_sender} to {_receiver}.";
    }
}