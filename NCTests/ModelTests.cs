using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NCBank.Models;

namespace NCTests  {
    [TestClass]
    public class ModelTests {

        /// <summary>
        /// Ensure that 100 unique tokens are generated
        /// </summary>
        [TestMethod]
        public void TestSessionTokenGenerate() {
            var tokens = new List<string>();
            for (var i = 0; i < 100; i++) {
                var sessionId = 
                    SessionManager.GetRandomString();
                while (tokens.Contains(sessionId)) {
                    sessionId = 
                        SessionManager.GetRandomString();
                }
                tokens.Add(sessionId);
            }

            var flag = 0;
            foreach (var token in tokens) {
                var count = tokens.Count(
                    checkToken => token == checkToken);
                if (count > 1) flag++;
            }

            Assert.AreEqual(0, flag, 
                $"Token Generation encountered ${flag} collisions.");
        }
    }
}
