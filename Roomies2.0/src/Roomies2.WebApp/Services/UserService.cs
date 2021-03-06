﻿using System;
using System.Threading.Tasks;
using Roomies2.DAL.Gateways;
using Roomies2.DAL.Model.People;
using Roomies2.DAL.Services;

namespace Roomies2.WebApp.Services
{
    public class UserService
    {
        public UserService(UserGateway userGateway, PasswordHasher passwordHasher)
        {
            Gateway = userGateway;
            Hasher = passwordHasher;
        }

        public UserGateway Gateway { get; }
        public PasswordHasher Hasher { get; }

        public Task<Result<int>> CreatePasswordUser(string email, string password)
        {
            return Gateway.CreatePasswordUser(email, Hasher.HashPassword(password));
        }

        public async Task<UserData> FindUser(string email, string password)
        {
            UserData account = await Gateway.FindByEmail(email);
            if (account != null && Hasher.VerifyHashedPassword(account.HashedPassword, password) == PasswordVerificationResult.Success)
            {
                return account;
            }

            return null;
        }
    }
}