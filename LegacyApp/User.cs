using System;

namespace LegacyApp
{
    public class User : Client
    {
        public DateTime DateOfBirth { get; internal set; }
        public string FirstName { get; internal set; }
        public bool HasCreditLimit { get; internal set; }
        public int CreditLimit { get; internal set; }
    }
}