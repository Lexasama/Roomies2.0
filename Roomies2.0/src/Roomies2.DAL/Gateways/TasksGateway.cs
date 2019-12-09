using Dapper;
using Roomies2.DAL.Model.People;
using Roomies2.DAL.Services;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;

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
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                TaskData task = await con.QueryFirstOrDefaultAsync<TaskData>(
                    @"SELECT * FROM rm2.tTasks t WHERE t.TaskId = @TaskId;",
                    new { TaskId = taskId });

                if (task == null) return Result.Failure<TaskData>(Status.NotFound, "Not found.");
                return Result.Success(task);
            }
        }

        public async Task<Result<int>> Create(string taskName, string des, DateTime date, int colocId)
        {
            if (!IsNameValid(taskName)) return Result.Failure<int>(Status.BadRequest, "The name is not valid");

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@TaskName", taskName);
                p.Add("@TaskDes", des);
                p.Add("@TaskDate", date);
                p.Add("@ColocId", colocId);
                p.Add("@TaskId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("rm2.sTasksCreate", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");

                Debug.Assert(status == 0);
                return Result.Success(Status.Created, p.Get<int>("@TaskId"));
            }
        }

        public async Task<Result> Delete(int taskId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@TaskId", taskId);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("rm2.sTasksDelete", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure(Status.NotFound, "Task not found");

                Debug.Assert(status == 0);
                return Result.Success();
            }
        }

        public async  Task<Result> Update(int taskId, string taskName, DateTime date, string des, bool state)
        {
            if (!IsNameValid(taskName)) return Result.Failure<int>(Status.BadRequest, "The name is not valid");

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@TaskId", taskId);
                p.Add("@TaskName", taskName);
                p.Add("@TaskDate", date);
                p.Add("@TaskDes", des);
                p.Add("@State", state);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("rm2.sTasksUpdate", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure(Status.NotFound, "Not found.");

                Debug.Assert(status == 0);
                return Result.Success(Status.Ok);
            }
        }

        public async Task<Result> Assign(int taskId, int roomieId)
        {

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@TaskId", taskId);
                p.Add("@RoomieId", roomieId);

                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("rm2.sTaskAssign", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure(Status.BadRequest, "Task already assigned to this roomie");

                Debug.Assert(status == 0);
                return Result.Success(Status.Ok);
            }

        }

        public async Task<Result> UpdateState(int taskId)
        {

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@TaskId", taskId);
                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("rm2.sTasksUpdateState", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure(Status.BadRequest, "Task does not exist");

                Debug.Assert(status == 0);
                return Result.Success(Status.Ok);
            }
        }
        
        bool IsNameValid(string name) => !string.IsNullOrWhiteSpace(name);

    }
}
