using System.Security.Cryptography;
using System.Text;
using System;
namespace NCBank.Models {
    internal static class Hashing {
        internal static string Hash(string password) {
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

        static internal bool VerifyMd5Hash(MD5 md5Hash, string input, string hash) {
            string hashOfInput = GetMd5Hash(md5Hash, input);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(hashOfInput, hash))
                return true;
            else
                return false;
        }
    }
}