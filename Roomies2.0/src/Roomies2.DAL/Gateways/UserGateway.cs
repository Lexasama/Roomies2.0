#region
using System.Collections.Generic;
using System.Threading.Tasks;
using Roomies2.DAL.Model.People;
using Roomies2.DAL.Services;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Dapper;

#endregion

namespace Roomies2.DAL.Gateways
{
    public class UserGateway
    {
        readonly string _connectionString;

        public UserGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string ConnectionString { get; }


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
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return await con.QueryFirstOrDefaultAsync<UserData>(
                    "select u.UserId, u.UserName, u.Email, u.Password, u.FacebookId, u.FacebookRefreshToken, u.GoogleId, u.GoogleRefreshToken from rm2.vUser u where u.UserId = @UserId",
                    new { UserId = userId });
            }
        }

        public async  Task<UserData> FindByFacebookId(string facebookId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return await con.QueryFirstOrDefaultAsync<UserData>(
                    "select * from rm2.vUser u where u.faceBookId = @FacebookId",
                    new { FaceBookId = facebookId});
            }
        }
               
        public async Task<UserData> FindByEmail(string email)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return await con.QueryFirstOrDefaultAsync<UserData>(
                    "select * from rm2.vUser u where u.Email = @Email",
                    new {Email = email});
            }
        }

        public async Task<UserData> FindByGoogleId(string googleId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return await con.QueryFirstOrDefaultAsync<UserData>(
                    "select    rm2.vUser u where u.GoogleId = @GoogleId",
                    new { GoogleId = googleId });
            }
        }

        //TODO
        public async Task<Result<int>> CreatePasswordUser(string userName, string email, byte[] hashedPassword)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@UserName", userName);
                p.Add("@Email", email);
                p.Add("@Password", hashedPassword);
                p.Add("@UserId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("rm2.sPasswordUserCreate", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure<int>(Status.BadRequest, "An account with this email already exists.");
                if (status == 2) return Result.Failure<int>(Status.BadRequest, "An account with this username already exists.");
                
                Debug.Assert(status == 0);
                return Result.Success(p.Get<int>("@UserId"));
            }
        }

        public async Task<UserData> FindUserName(string userName)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return await con.QueryFirstOrDefaultAsync<UserData>(
                    "select * from rm2.vUser u where u.UserName = @UserName",
                    new { UserName = userName });
            }
        }

        public async Task CreateOrUpdateFacebookUser(string userName, string email, string facebookId, string refreshToken)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.ExecuteAsync(
                    "rm2.sFacebookUserCreateOrUpdate",
                    new { UserName = userName, Email = email, FacebookId = facebookId, RefreshToken = refreshToken },
                    commandType: CommandType.StoredProcedure);
            }
        }
        public async Task CreateOrUpdateGoogleUser(string userName, string email, string googleId, string refreshToken)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.ExecuteAsync(
                    "rm2.sGoogleUserCreateOrUpdate",
                    new { UserName = userName, Email = email, GoogleId = googleId, RefreshToken = refreshToken },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<string>> GetAuthenticationProviders(string userId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return await con.QueryAsync<string>(
                    "select p.ProviderName from rm2.vAuthenticationProvider p where p.UserId = @UserId",
                    new { UserId = userId });
            }
        }

        public async Task Delete(int userId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.ExecuteAsync("rm2.sUserDelete", new { UserId = userId }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateEmail(int userId, string email)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.ExecuteAsync(
                    "rm2.sUserUpdate",
                    new { UserId = userId, Email = email },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdatePassword(int userId, byte[] password)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                await con.ExecuteAsync(
                    "rm2.sPasswordUserUpdate",
                    new { UserId = userId, Password = password },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}