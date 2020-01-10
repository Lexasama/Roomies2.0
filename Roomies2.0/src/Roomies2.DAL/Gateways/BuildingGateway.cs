using Dapper;
using Roomies2.DAL.Model.BuildingManagement;
using Roomies2.DAL.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Roomies2.DAL.Gateways
{
    public class BuildingGateway
    {
        readonly string _connectionString;

        public BuildingGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Result<int>> Create(string name, string addresse, int supervisorId)
        {
            if (!IsNameValid(name)) return Result.Failure<int>(Status.BadRequest, "The Building name is not valid");
            if (!IsNameValid(addresse)) return Result.Failure<int>(Status.BadRequest, "The address is not valid");

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@BuildingName", name);
                p.Add("@BuildingAddress", addresse);
                p.Add("@SupervisorId", supervisorId);
                p.Add("@BuildingId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("rm2.sBuildingCreate", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure<int>(Status.BadRequest, "This Building already exists");

                Debug.Assert(status == 0);
                return Result.Success(Status.Created, p.Get<int>("@BuildingId"));
            }
        }

        public async Task<Result<BuildingData>> Find(int buildingId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                BuildingData super = await con.QueryFirstOrDefaultAsync<BuildingData>(
                    @"SELECT * FROM rm2.tBuilding b WHERE b.BuildingId = @BuildingId;",
                    new { BuildingId = buildingId }
                    );

                if (super == null) return Result.Failure<BuildingData>(Status.NotFound, "Building not found.");
                return Result.Success(Status.Ok, super);

            }
        }

        public async Task<Result> Update(int buildingId, string buildingName, string addresse)
        {
            if (!IsNameValid(buildingName)) return Result.Failure<int>(Status.BadRequest, "The Building name is not valid");

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@BuildingId", buildingId);
                p.Add("@BuildingName", buildingName);
                p.Add("@BuildingAddress", addresse);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("rm2.sBuildingUpdate", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure<int>(Status.NotFound, "Not found");

                Debug.Assert(status == 0);
                return Result.Success();
            }
        }

        public async Task<Result> Delete(int buildingId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@BuildingId", buildingId);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("rm2.sBuildingDelete", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure(Status.NotFound, "Building not found");

                Debug.Assert(status == 0);
                return Result.Success();
            }
        }

        bool IsNameValid(string name) => !string.IsNullOrWhiteSpace(name);
    }
}
