#region

using System.Collections.Generic;
using System.Threading.Tasks;
using Roomies2.DAL.Model.People;
using Roomies2.DAL.Services;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Dapper;
using System;

#endregion

namespace Roomies2.DAL.Gateways
{
    public class UserGateway
    {
        public string ConnectionString { get; }

        public UserGateway(string connectionString) => ConnectionString = connectionString;


        //public async Task<IAccountData> FindById(int userId)
        //{
        //    using(SqlConnection con = new SqlConnection(_connectionString))
        //    {
        //        return await con.QueryFirstOrDefaultAsync<IAccountData>(
        //            "select u.UserId, u.Email, u.  from rm2.vUser where u.UserId = @UserId",
        //            new {UsierId = userId});
        //        //TODO
        //    }
        //}

        public async Task<UserData> FindById(int userId)
        {
            await using var con = new SqlConnection(ConnectionString);
            return await con.QueryFirstOrDefaultAsync<UserData>(
                "select * from rm2.vUser u where u.UserId = @UserId",
                new {UserId = userId});
        }

        public async Task<UserData> FindByFacebookId(string facebookId)
        {
            await using var con = new SqlConnection(ConnectionString);
            return await con.QueryFirstOrDefaultAsync<UserData>(
                "select * from rm2.vUser u where u.faceBookId = @FacebookId",
                new {FaceBookId = facebookId});
        }

        public async Task<UserData> FindByEmail(string email)
        {
            await using var con = new SqlConnection(ConnectionString);
            var r = await con.QueryFirstOrDefaultAsync<UserData>(
                "select * from rm2.vUser u where u.Email = @Email",
                new {Email = email});

            return r;
        }

        public async Task<UserData> FindByGoogleId(string googleId)
        {
            await using var con = new SqlConnection(ConnectionString);
            return await con.QueryFirstOrDefaultAsync<UserData>(
                "select  * FROM  rm2.vUser u WHERE u.GoogleId = @GoogleId",
                new {GoogleId = googleId});
        }

        public async Task<Result<int>> CreatePasswordUser(string userName, string email, string lastName,
            string firstName, string phone, int sex, DateTime birthDate, byte[] hashedPassword)
        {
            await using var con = new SqlConnection(ConnectionString);
            var p = new DynamicParameters();
            p.Add("@UserName", userName);
            p.Add("@Email", email);
            p.Add("@LastName", lastName);
            p.Add("@FirstName", firstName);
            p.Add("@Phone", phone);
            p.Add("@Sex", sex);
            p.Add("@BirthDate", birthDate);
            p.Add("@HashedPassword", hashedPassword);
            p.Add("@UserId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            await con.ExecuteAsync("rm2.sPasswordUserCreate", p, commandType: CommandType.StoredProcedure);

            var status = p.Get<int>("@Status");
            switch (status)
            {
                case 1:
                    return Result.Failure<int>(Status.BadRequest, "An account with this email already exists.");
                case 2:
                    return Result.Failure<int>(Status.BadRequest, "An account with this username already exists.");
                default:
                    Debug.Assert(status == 0);
                    return Result.Success(p.Get<int>("@UserId"));
            }
        }

        public async Task<UserData> FindUserName(string userName)
        {
            await using var con = new SqlConnection(ConnectionString);
            return await con.QueryFirstOrDefaultAsync<UserData>(
                "select * from rm2.vUser u where u.UserName = @UserName",
                new {UserName = userName});
        }

        private async Task<Result<int>> CreateUser(string userName, string email)
        {
            await using var con = new SqlConnection(ConnectionString);

            var p = new DynamicParameters();
            p.Add("@Email", email);
            p.Add("@UserName", userName);
            p.Add("@UserId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            await con.ExecuteAsync("rm2.sUserCreate", p, commandType: CommandType.StoredProcedure);
            var status = p.Get<int>("@Status");

            return status == 0
                ? Result.Success(Status.Ok, p.Get<int>(("@UserId")))
                : Result.Failure<int>(Status.BadRequest, "User already exists");
        }

        public async Task CreateOrUpdateFacebookUser(string userName, string email, string facebookId,
            string refreshToken)
        {
            UserData r = FindByEmail(email).Result;

            // Coalescing expression meaning if r.userId is null taking CreateUser().Result.Content
            int userId = r?.UserId ?? CreateUser(userName, email).Result.Content;

            await using var con = new SqlConnection(ConnectionString);

            var p = new DynamicParameters();
            p.Add("@UserId", userId);
            p.Add("@FacebookId", facebookId);
            p.Add("@RefreshToken", refreshToken);
            p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            await con.ExecuteAsync("rm2.sFacebookUserCreateOrUpdate", p, commandType: CommandType.StoredProcedure);
        }

        public async Task CreateOrUpdateGoogleUser(string userName, string email, string googleId, string refreshToken)
        {
            UserData r = FindByEmail(email).Result;

            // Coalescing expression meaning if r.userId is null taking CreateUser().Result.Content
            int userId = r?.UserId ?? CreateUser(userName, email).Result.Content;

            await using var con = new SqlConnection(ConnectionString);

            var p = new DynamicParameters();
            p.Add("@UserId", userId);
            p.Add("@GoogleId", googleId);
            p.Add("@RefreshToken", refreshToken);
            p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            await con.ExecuteAsync("rm2.sGoogleUserCreateOrUpdate", p, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<string>> GetAuthenticationProviders(string userId)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                return await con.QueryAsync<string>(
                    "select p.ProviderName from rm2.vAuthenticationProvider p where p.UserId = @UserId",
                    new {UserId = userId});
            }
        }

        public async Task Delete(int userId)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                await con.ExecuteAsync("rm2.sUserDelete", new {UserId = userId},
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateEmail(int userId, string email)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                await con.ExecuteAsync(
                    "rm2.sUserUpdate",
                    new {UserId = userId, Email = email},
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdatePassword(int userId, byte[] password)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                await con.ExecuteAsync(
                    "rm2.sPasswordUserUpdate",
                    new {UserId = userId, Password = password},
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}