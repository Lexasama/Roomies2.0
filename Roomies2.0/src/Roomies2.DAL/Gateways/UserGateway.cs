﻿#region

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using Dapper;
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
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                return await con.QueryFirstOrDefaultAsync<UserData>(
                    "select * FROM  rm2.vUser u WHERE u.GoogleId = @GoogleId",
                    new { GoogleId = googleId });
            }
        }
    

        public async Task<Result<int>> CreatePasswordUser(string email, byte[] hashedPassword)
        {
            await using SqlConnection con = new SqlConnection(ConnectionString);
            DynamicParameters p = new DynamicParameters();
            p.Add("@Email", email);
            p.Add("@HashedPassword", hashedPassword);
            p.Add("@UserId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            await con.ExecuteAsync("rm2.sPasswordUserCreate", p, commandType: CommandType.StoredProcedure);

            int status = p.Get<int>("@Status");
            if (status == 1) return Result.Failure<int>(Status.BadRequest, "An account with this email already exists.");
            
            Debug.Assert(status == 0);
            return Result.Success(p.Get<int>("@UserId"));
        }

        public async Task<UserData> FindUserName(string userName)
        {
            await using var con = new SqlConnection(ConnectionString);
            return await con.QueryFirstOrDefaultAsync<UserData>(
                "select * from rm2.vUser u where u.UserName = @UserName",
                new {UserName = userName});
        }

        public async Task<Result<int>> CreateUser(string userName, string email)
        {
            await using SqlConnection con = new SqlConnection(ConnectionString);

            DynamicParameters p = new DynamicParameters();
            p.Add("@Email", email);
            p.Add("@UserName", userName);
            p.Add("@UserId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            await con.ExecuteAsync("rm2.sUserCreate", p, commandType: CommandType.StoredProcedure);
            int status = p.Get<int>("@Status");

            return status == 0
                ? Result.Success(Status.Ok, p.Get<int>("@UserId"))
                : Result.Failure<int>(Status.BadRequest, "User already exists");
        }

        public async Task CreateOrUpdateFacebookUser( string email, string facebookId, string refreshToken)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString)){
                await con.ExecuteAsync("rm2.sFacebookUserCreateOrUpdate",
                    new { Email = email, FacebookId = facebookId, RefreshToken = refreshToken },
                    commandType: CommandType.StoredProcedure);
            }

        }

        public async Task CreateOrUpdateGoogleUser(string email, string googleId, string refreshToken)
        {

            using (  SqlConnection con = new SqlConnection(ConnectionString))
            {
                await con.ExecuteAsync("rm2.sGoogleUserCreateOrUpdate", 
                    new { Email = email, GoogleId = googleId, RefreshToken = refreshToken },
                    commandType: CommandType.StoredProcedure);
            }

        }

        public async Task<IEnumerable<string>> GetAuthenticationProviders(string userId)
        {
            using (var con = new SqlConnection(ConnectionString))
            {
                return await con.QueryAsync<string>(
                    "select p.ProviderName from rm2.vAuthenticationProvider p where p.UserId = @UserId",
                    new {UserId = userId});
            }
        }

        public async Task Delete(int userId)
        {
            using (var con = new SqlConnection(ConnectionString))
            {
                await con.ExecuteAsync("rm2.sUserDelete",
                    new {UserId = userId},
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateEmail(int userId, string email)
        {
            using (var con = new SqlConnection(ConnectionString))
            {
                await con.ExecuteAsync(
                    "rm2.sUserUpdate",
                    new {UserId = userId, Email = email},
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdatePassword(int userId, byte[] password)
        {
            using (var con = new SqlConnection(ConnectionString))
            {
                await con.ExecuteAsync(
                    "rm2.sPasswordUserUpdate",
                    new {UserId = userId, Password = password},
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}