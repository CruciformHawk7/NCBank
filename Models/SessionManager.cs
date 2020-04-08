using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using MongoDB.Bson;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NCBank.Models {
    public static class SessionManager {
        static private Dictionary<string, BankCustomer> sessions = new Dictionary<string, BankCustomer>();

        static public string GetRandomString(int size = 13) {
            return new string(Enumerable.Repeat("abcdef1234567890", size)
                .Select(s => {
                    var cryptoResult = new byte[4];
                    using (var cryptoProvider = new RNGCryptoServiceProvider())
                        cryptoProvider.GetBytes(cryptoResult);

                    return s[new Random(BitConverter.ToInt32(cryptoResult, 0)).Next(s.Length)];
                })
                .ToArray());
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
            sessions.Add(sessionID, customer);
            return session;
        }

        public static BankCustomer GetSession(string sessionID) {
            return sessions[sessionID];
        }
    }
}