using System;
using NCBank.Models;

namespace NCTests {
    public class MockUserGenerator {
        private RandomNameGenerator _nameGen = new RandomNameGenerator();

        private static string GeneratePan() {
            var part1 = SessionManager.GetRandomString(RandomType.Caps, 5);
            var part2 = SessionManager.GetRandomString(RandomType.Numeric, 4);
            var part3 = SessionManager.GetRandomString(RandomType.Small, 1);
            return part1 + part2 + part3;
        }

        public BankCustomer GetMockUser() {
            var name = _nameGen.GenerateName();
            return new BankCustomer {
                FirstName = name[0], LastName = name[1],
                Aadhar = SessionManager.GetRandomString(RandomType.Numeric, 12), 
                Age = new Random().Next(20, 80),
                City = "Bengaluru", FirstAddress = "Address",
                DateCreated = DateTime.Now.Date,
                Email = $"{name[0].ToLower()}{name[1].ToLower()}@{name[0].ToLower()}industries.com",
                Gender = "male", HouseName = $"{name[0]}'s Palace",
                HouseNumber = "4556", JobTitle = "Manager",
                MaritalStatus = "married", 
                OrgCity = "Bengaluru",
                OrgName = $"{name[0]} Industries", 
                OrgPhone = SessionManager.GetRandomString(RandomType.Numeric,
                    new Random().Next(10,15)),
                Phone = SessionManager.GetRandomString(RandomType.Numeric, 
                    new Random().Next(10,15)),
                Pan = GeneratePan(),
                Password = SessionManager.GetRandomString(RandomType.AlphaNumeric), 
                State = "Karnataka", SecondAddress = "Main Road", Verified = true
            };
        }
    }
}