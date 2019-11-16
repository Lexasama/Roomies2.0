﻿using Roomies2.DAL.Services;
using System.Threading.Tasks;
using Roomies2.DAL.Model.BuildingManagement;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using Dapper;
using System;

namespace Roomies2.DAL.Gateways
{
    public class ColocGateway
    {
        public string ConnectionString { get; }

        public ColocGateway(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public async Task<Result<ColocData>> FindById(int colocId)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                ColocData c = await con.QueryFirstOrDefaultAsync<ColocData>(
                    @"SELECT ColocName, PicPath FROM rm2.tColoc c WHERE c.ColocId = @ColocId;",
                    new { ColocId = colocId });

                if (c == null) return Result.Failure<ColocData>(Status.NotFound, "Not found.");
                return Result.Success(c);
            }
        }

        public async Task<Result<int>> Create(int roomieId, string name, string picPath)
        {
            if (!IsNameValid(name)) return Result.Failure<int>(Status.BadRequest, "The name is not valid");

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                var p = new DynamicParameters();
                p.Add("@ColocName", name);
                p.Add("@RoomieId", roomieId);
                p.Add("@PicPath", picPath);
                p.Add("@RoomieId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("rm2.sColocCreate", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");
                
                Debug.Assert(status == 0);
                return Result.Success(Status.Created, p.Get<int>("@ColocId"));
            }
        }

        public Task<Result<int>> Create(int roomieId, object colocName, object picPath)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> Delete(int colocId)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                var p = new DynamicParameters();
                p.Add("@ColocId", colocId);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("rm2.sColocDelete", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure(Status.NotFound, "Coloc not found");

                Debug.Assert(status == 0);
                return Result.Success();
            }
        }

        public async Task<Result> Update(string name, string picPath)
        {
            if (!IsNameValid(name)) return Result.Failure<int>(Status.BadRequest, "The Name is not valid");

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                var p = new DynamicParameters();
                p.Add("@ColocName", name);
                p.Add("@PicPath", picPath);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("rm2.sColocUpdate", p, commandType: CommandType.StoredProcedure);
                
                int status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure(Status.NotFound, "Not found.");

                Debug.Assert(status == 0);
                return Result.Success(Status.Ok);
            }
        }

        bool IsNameValid(string name) => string.IsNullOrWhiteSpace(name);
    }
}