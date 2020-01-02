using Dapper;
using Roomies2.DAL.Model.People;
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
    public class SupervisorGateway
    {
        readonly string _connectionString;

        public SupervisorGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Result<int>> Create(int supervisorId, string lastName, string firstName, string phone)
        {
            if (!IsNameValid(lastName)) return Result.Failure<int>(Status.BadRequest, "The lastname is not valid");
            if (!IsNameValid(firstName)) return Result.Failure<int>(Status.BadRequest, "The firstname is not valid");

            using(SqlConnection con = new SqlConnection(_connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@SupervisorId", supervisorId);
                p.Add("@LastName", lastName);
                p.Add("@FirstName", firstName);
                p.Add("@Phone", phone);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("rm2.sSupervisorCreate", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure<int>(Status.BadRequest, "This supervisor already exists");

                Debug.Assert(status == 0);
                return Result.Success(Status.Created, p.Get<int>("@SupervisorId"));
            }
        }

        public async Task<Result<SupervisorData>> Find(int superId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SupervisorData super = await con.QueryFirstOrDefaultAsync<SupervisorData>(
                    @"SELECT * FROM rm2.tSupervisor s WHERE s.SupervisorId = @SupervisorId;",
                    new { SupervisorId = superId }
                    );

                if (super == null) return Result.Failure<SupervisorData>(Status.NotFound, "Supervisor not found.");
                return Result.Success(Status.Ok, super);

            }
        }

        public async Task<Result> Update(int supervisorId, string email, string lastName, string firstName, string phone)
        {
            if (!IsNameValid(lastName)) return Result.Failure<int>(Status.BadRequest, "The lastname is not valid");
            if (!IsNameValid(firstName)) return Result.Failure<int>(Status.BadRequest, "The firstname is not valid");

            using( SqlConnection con = new SqlConnection(_connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@SupervisorId", supervisorId);
                p.Add("@LastName", lastName);
                p.Add("@FirstName", firstName);
                p.Add("@Phone", phone);
                p.Add("@Email", email);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("rm2.sSupervisorUpdate", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure<int>(Status.NotFound, "Not found");

                Debug.Assert(status == 0);
                return Result.Success();
            }

        }

        public async Task<Result> Delete(int superId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@SupervisorId", superId);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("rm2.sSupervisorDelete", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure(Status.NotFound, "Supervisor not found");

                Debug.Assert(status == 0);
                return Result.Success();
            }
        }

        bool IsNameValid(string name) => !string.IsNullOrWhiteSpace(name);
    }
}
