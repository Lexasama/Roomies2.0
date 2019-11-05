using System.Threading.Tasks;
using Roomies2.DAL.Gateways;
using Roomies2.DAL.Model.People;
using Roomies2.DAL.Services;

namespace Roomies2.WebApp.Services
{
    public class UserService
    {
        readonly UserGateway _userGateway;
        readonly PasswordHasher _passwordHasher;

        public UserService(UserGateway userGateway, PasswordHasher passwordHasher)
        {
            _userGateway = userGateway;
            _passwordHasher = passwordHasher;
        }

        public Task<Result<int>> CreatePasswordUser(string email, string password)
        {
            return _userGateway.CreatePasswordUser(email, _passwordHasher.HashPassword(password));
        }

        public async Task<IAccountData> FindUser(string email, string password)
        {
            IAccountData account = await _userGateway.FindByEmail(email);
            if (account != null && _passwordHasher.VerifyHashedPassword(account.Password, password) == PasswordVerificationResult.Success)
            {
                return account;
            }

            return null;
        }
    }
}
