﻿using Roomies2.DAL.Model.People;
using Roomies2.DAL.Services;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using Dapper;
using System;

namespace Roomies2.DAL.Gateways
{
    public class RoomieGateway
    {
        readonly string _connectionString;

        public RoomieGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Result<RoomieData>> FindById(int roomieId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                RoomieData r = await con.QueryFirstOrDefaultAsync<RoomieData>(
                    @"SELECT * FROM rm2.vRoomie r WHERE r.RoomieId = @RoomieId;",
                    new { RoomieId = roomieId }
                    );

                if (r == null) return Result.Failure<RoomieData>(Status.NotFound, "Roomie was not found.");
                return Result.Success(Status.Ok, r);

            }

        }


        public async Task<Result<RoomieProfile>> GetProfile(int roomieId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                RoomieProfile profile = await con.QueryFirstOrDefaultAsync<RoomieProfile>(
                    @"SELECT * FROM rm2.vRoomie r where r.RoomieId = @RoomieId",
                    new { RoomieId = roomieId }
                        );
                if (profile == null) return Result.Failure<RoomieProfile>(Status.NotFound, "Roomie not found.");
                return Result.Success(Status.Ok, profile);
            }

        }

        public async Task<Result<UserData>> FindUserByEmail(string email)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                UserData user = await con.QueryFirstOrDefaultAsync<UserData>(
                    @"SELECT * FROM rm2.vUser u WHERE u.Email = @Email;",
                    new { Email = email }
                    );
                if (user == null) return Result.Failure<UserData>(Status.NotFound, "User was not found.");
                return Result.Success(Status.Ok, user);
            }
        }

        public async Task<Result<RoomieData>> FindRoomieByEmail(string email)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                RoomieData r = await con.QueryFirstOrDefaultAsync<RoomieData>(
                    @"SELECT * FROM rm2.vRoomie u WHERE u.Email = @Email;",
                    new { Email = email }
                    );
                if (r == null) return Result.Failure<RoomieData>(Status.NotFound, "Roomie was not found.");
                return Result.Success(Status.Ok, r);
            }

        }

        /// <summary>
        /// When registered via External provider
        /// </summary>
        /// <param name="roomieId"></param>
        /// <param name="lastName"></param>
        /// <param name="firstName"></param>
        /// <param name="phone"></param>
        /// <param name="sex"></param>
        /// <param name="birthDate"></param>
        /// <param name="desc"></param>
        /// <param name="pic"></param>
        /// <returns></returns>
        public async Task<Result<int>> Create(int roomieId, string lastName, string firstName, string phone, int sex, DateTime birthDate, string desc, string pic)
        {
            if (!IsNameValid(lastName)) return Result.Failure<int>(Status.BadRequest, "The lastname is not valid");
            if (!IsNameValid(firstName)) return Result.Failure<int>(Status.BadRequest, "The firstname is not valid");
           
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@FirstName", firstName);
                p.Add("@LastName", lastName);
                p.Add("@Phone", phone);
                p.Add("@Description", desc);
                p.Add("@Sex", sex);
                p.Add("@BirthDate", birthDate);
                p.Add("@PicturePath", pic);
                p.Add("@RoomieId", roomieId);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("rm2.sRoomieCreateUpdate", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure<int>(Status.BadRequest, "A Roomie with this username already exists");

                Debug.Assert(status == 0);
                return Result.Success(Status.Created, p.Get<int>("@RoomieId"));
            }
        }


        public async Task<Result> Update(int roomieId, string userName, string lastName, string firstName, string phone, int sex, DateTime birthDate, string desc, string pic)
        {
            if (!IsNameValid(lastName)) return Result.Failure<int>(Status.BadRequest, "The lastname is not valid");
            if (!IsNameValid(firstName)) return Result.Failure<int>(Status.BadRequest, "The firstname is not valid");

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@UserName", userName);
                p.Add("@FirstName", firstName);
                p.Add("@LastName", lastName);
                p.Add("@Phone", phone);
                p.Add("@Description", desc);
                p.Add("@Sex", sex);
                p.Add("@BirthDate", birthDate);
                p.Add("@PicturePath", pic);
                p.Add("@RoomieId", roomieId);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("rm2.sRoomieUpdate", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure<int>(Status.BadRequest, "A Roomie with this username already exists");

                Debug.Assert(status == 0);
                return Result.Success();
            }
        }

        public async Task<Result> Delete(int roomieId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@RoomieId", roomieId);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("rm2.sRoomieDelete", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure(Status.NotFound, "Roomie not found");

                Debug.Assert(status == 0);
                return Result.Success();
            }
        }

        bool IsNameValid(string name) => !string.IsNullOrWhiteSpace(name);
    }
}


