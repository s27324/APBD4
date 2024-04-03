﻿using System;

namespace LegacyApp
{
    public class UserService
    {

        private IClientRepository ClientRepository;
        private IUserCreditService UserCreditService;
        
        public UserService()
        {
            ClientRepository = new ClientRepository();
            UserCreditService = new UserCreditService();
        }

        public UserService(IClientRepository clientRepository, IUserCreditService userCreditService)
        {
            ClientRepository = clientRepository;
            UserCreditService = userCreditService;
        }


        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (NameNullOrEmpty(firstName, lastName) || AndOrDotMissingInEmail(email) || CheckingAge(dateOfBirth))
            {
                return false;
            }
            
            var client = ClientRepository.GetById(clientId);

            var user = CreatingNewUser(client, firstName, lastName, email, dateOfBirth);

            DeterminingClientData(client, user);

            if (CheckingCreditLimit(user))
            {
                return false;
            }
            
            UserDataAccess.AddUser(user);
            return true;
        }

        public bool NameNullOrEmpty(string firstName, string lastName)
        {
            return string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName);
        }

        public bool AndOrDotMissingInEmail(string email)
        {
            return !email.Contains("@") && !email.Contains(".");
        }

        public bool CheckingAge(DateTime dateTime)
        {
            var now = DateTime.Now;
            int age = now.Year - dateTime.Year;
            if (now.Month < dateTime.Month || (now.Month == dateTime.Month && now.Day < dateTime.Day)) age--;
            return age < 21;
        }

        public User CreatingNewUser(Client client, string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            return new User{
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };
        }

        public void DeterminingClientData(Client client, User user)
        {
            if (client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else if (client.Type == "ImportantClient")
            {
                int creditLimit = UserCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                creditLimit *= 2;
                user.CreditLimit = creditLimit;
            }
            else
            {
                user.HasCreditLimit = true;
                int creditLimit = UserCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                user.CreditLimit = creditLimit;
            }
        }

        public bool CheckingCreditLimit(User user)
        {
            return user.HasCreditLimit && user.CreditLimit < 500;
        }
    }
}
