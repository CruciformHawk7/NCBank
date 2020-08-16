using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NCBank.Models {
    public static class SessionManager {
        static private Dictionary<string, ObjectId> sessions = new Dictionary<string, ObjectId>();

        static public string GetRandomString(int size = 13, string chunk="abcdef1234567890") {
            return new string(Enumerable.Repeat(chunk, size)
                .Select(s => {
                    var cryptoResult = new byte[4];
                    using (var cryptoProvider = new RNGCryptoServiceProvider())
                        cryptoProvider.GetBytes(cryptoResult);

                    return s[new Random(BitConverter.ToInt32(cryptoResult, 0)).Next(s.Length)];
                })
                .ToArray());
        }

        public static string GetRandomString(RandomType randomType, int size = 13) {
            return randomType switch {
                RandomType.AlphaNumeric => GetRandomString(size, "abcdef1234567890"),
                RandomType.Numeric => GetRandomString(size, "1234567890"),
                RandomType.Caps => GetRandomString(size, "ABCDEFGHIJKLMNOPQRSTUVWXYZ"),
                RandomType.Small => GetRandomString(size, "abcdefghijklmnopqrstuvwxyz"),
                _ => ""
            };
        }

        public static async Task<Sessions> InsertSession(BankCustomer customer) {
            string sessionID = GetRandomString();
            while (sessions.ContainsKey(sessionID)) {
                sessionID = GetRandomString();
            }
            
            Sessions session = new Sessions();
            session.SessionID = sessionID;
            session.Email = customer.Email;
            await DBInterface.sess.InsertOneAsync(session);
            sessions.Add(sessionID, customer.Id);
            return session;
        }

        public static BankCustomer GetSession(string sessionID) {
            var id = sessions[sessionID];
            var filter = Builders<BankCustomer>.Filter.Eq("_id", id);
            return DBInterface.cust.Find(filter).Single();
        }

        public static void RemoveSession(string sessionID) {
            var filter = Builders<Sessions>.Filter.Eq("_id", sessions[sessionID]);
            DBInterface.sess.DeleteOne(filter);
            sessions.Remove(sessionID);
        }
    }

    public enum RandomType {
        AlphaNumeric,
        Numeric,
        Caps,
        Small
    }
}