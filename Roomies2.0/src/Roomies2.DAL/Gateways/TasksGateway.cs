using Dapper;
using Roomies2.DAL.Model.People;
using Roomies2.DAL.Services;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Roomies2.DAL.Gateways
{
    public class TasksGateway
    {
        public string _connectionString;

        public TasksGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Result<TaskData>> Find(int taskId)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<int>> Create(string taskName, string description, DateTime date1, DateTime date2, int colocId )
        {

        }

        public async Task<Result<int>> Create(string taskName, string des, DateTime date, int colocId)
        {
            if (!IsNameValid(taskName)) return Result.Failure<int>(Status.BadRequest, "The name is not valid");

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@TaskName", taskName);
                p.Add("@Description", des);
                p.Add("@Date", date);
                p.Add("@ColocId", colocId);
                p.Add("@TaskId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("rm2.sTaskCreate", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");

                Debug.Assert(status == 0);
                return Result.Success(Status.Created, p.Get<int>("@TaskId"));
            }
        }

        public Task<Result> Delete(int taskId)
        {
            throw new NotImplementedException();
        }

        public Task<Result> Update(string taskName, DateTime date, string des)
        {
            throw new NotImplementedException();
        }

        bool IsNameValid(string name) => !string.IsNullOrWhiteSpace(name);

    }
}
