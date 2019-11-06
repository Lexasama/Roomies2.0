#region

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Roomies2.DAL.Model.People;
using Roomies2.DAL.Services;

#endregion

namespace Roomies2.DAL.Gateways
{
    public class UserGateway
    {
        public UserGateway(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; }

        public async Task CreateOrUpdateFacebookUser(string userInfoEmail, string userInfoFacebookId, string userInfoRefreshToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IAccountData> FindByFacebookId(string facebookId)
        {
            throw new NotImplementedException();
        }

        public Task CreateOrUpdateFacebookUser(string email, string facebookId, string refreshToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<IAccountData>> FindGitHubUser(int userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IAccountData> FindByGoogleId(string userInfoGoogleId)
        {
            throw new System.NotImplementedException();
        }

        public async Task CreateOrUpdateGoogleUser(string userInfoEmail, string userInfoGoogleId, string userInfoRefreshToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<string>> GetAuthenticationProviders(string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<int>> CreatePasswordUser(string email, byte[] hashPassword)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IAccountData> FindByEmail(string email)
        {
            throw new System.NotImplementedException();
        }
    }
}