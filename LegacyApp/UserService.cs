using System;
using System.Collections.Generic;

namespace LegacyApp
{
    public class UserService
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            // if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            // {
            //     return false;
            // }
            //
            // if (!email.Contains("@") && !email.Contains("."))
            // {
            //     return false;
            // }
            //
            // var now = DateTime.Now;
            // int age = now.Year - dateOfBirth.Year;
            // if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;
            //
            // if (age < 21)
            // {
            //     return false;
            // }

            if (!CheckConditions(firstName, lastName, email, dateOfBirth, clientId)) return false;

            var clientRepository = new ClientRepository();
            var client = clientRepository.GetById(clientId);

            var user = new User
            {
                LastName = client.LastName,
                ClientId = client.ClientId,
                Address = client.Address,
                Type = client.Address,
                Email = client.Email,
                DateOfBirth = dateOfBirth,
                FirstName = firstName,
            };

            switch (client.Type)
            {
                case "VeryImportantClinet":
                    user.HasCreditLimit = false;
                    break;
                
                case "ImportantClient":
                    using (var userCreditService = new UserCreditService())
                    {
                        int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                        creditLimit *= 2;
                        user.CreditLimit = creditLimit;
                    }
                    break;
                
                default:
                    user.HasCreditLimit = true;
                    using (var userCreditService = new UserCreditService())
                    {
                        int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                        user.CreditLimit = creditLimit; 
                    }

                    break;
            }

            // if (client.Type == "VeryImportantClient")
            // {
            //     user.HasCreditLimit = false;
            // }
            // else if (client.Type == "ImportantClient")
            // {
            //     using (var userCreditService = new UserCreditService())
            //     {
            //         int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
            //         creditLimit *= 2;
            //         user.CreditLimit = creditLimit;
            //     }
            // }
            // else
            // {
            //     user.HasCreditLimit = true;
            //     using (var userCreditService = new UserCreditService())
            //     {
            //         int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
            //         user.CreditLimit = creditLimit; 
            //     }
            // }

            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }

        private int CalculateAge(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--; 
            return age;
        }

        private bool CheckConditions(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            List<Func<bool>> conditions = new List<Func<bool>>
            {
                () => string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName),
                () => email.Contains("@") && email.Contains("."),
                () => CalculateAge(dateOfBirth) >= 21
            };

            foreach (var condition in conditions)
            {
                if (!condition())
                {
                    return false;
                }
            }

            return true;
        }
    }
}
