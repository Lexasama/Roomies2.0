using System;
using System.Threading.Tasks;
using Roomies2.DAL.Gateways;
using Roomies2.DAL.Model.People;
using Roomies2.DAL.Services;

namespace Roomies2.WebApp.Services
{
    public class UserService
    {
        public UserGateway Gateway { get; }
        public PasswordHasher Hasher { get; }

        public UserService(UserGateway userGateway, PasswordHasher passwordHasher)
        {
            Gateway = userGateway;
            Hasher = passwordHasher;
        }

        public Task<Result<int>> CreatePasswordUser(string userName, string email, string lastName, string firstName, string phone, int sex, DateTime birthDate, string password)
        {
            return Gateway.CreatePasswordUser(userName,email, lastName, firstName, phone, sex, birthDate, Hasher.HashPassword(password));
        }

        public async Task<UserData> FindUser(string email, string password)
        {
            UserData account = await Gateway.FindByEmail(email);
            if (account != null && Hasher.VerifyHashedPassword(account.Password, password) == PasswordVerificationResult.Success)
            {
                return account;
            }

            return null;
        }
    }
}
