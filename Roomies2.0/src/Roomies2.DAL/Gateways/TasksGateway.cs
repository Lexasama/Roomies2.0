using Dapper;
using Roomies2.DAL.Model;
using Roomies2.DAL.Model.People;
using Roomies2.DAL.Services;
using System;
using System.Collections.Generic;
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
                return Result.Success(Status.Ok, task);
            }
        }


        public async Task<IEnumerable<TaskData>> GetTasks(int colocId)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                
                return await con.QueryAsync<TaskData>(
                    @"SELECT * FROM rm2.tTasks WHERE ColocId = @ColocId;", 
                    new {ColocId = colocId });
            }
        }

        public async Task<IEnumerable<TaskData>> GetTasks(int colocId, bool isActive)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                if (isActive)
                {
                    return await con.QueryAsync<TaskData>(
                    @"SELECT * FROM rm2.tTasks t WHERE t.ColocId = @ColocId and t.State = 0;",
                    new { ColocId = colocId });
                }
                else { 
               
                    return await con.QueryAsync<TaskData>(
                    @"SELECT * FROM rm2.tTasks t WHERE t.ColocId = @ColocId and t.State = 1;",
                    new { ColocId = colocId });
                }

            }
        }

        /// <summary>
        /// Get roomieId and firstName of Roomies assigned to a task
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TaskRoomies>> GetAssignedRoomies(int taskId)  
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                return await con.QueryAsync<TaskRoomies>(
                    @"SELECT firstName, roomieId FROM rm2.vTaskRoomies WHERE TaskId = @TaskId;",
                    new { TaskId = taskId });
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

        public async Task<Result> Unassign(int taskId, int roomieId)
        {

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var p = new DynamicParameters();
                p.Add("@TaskId", taskId);
                p.Add("@RoomieId", roomieId);

                p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                await con.ExecuteAsync("rm2.sTaskUnassign", p, commandType: CommandType.StoredProcedure);

                int status = p.Get<int>("@Status");
                if (status == 1) return Result.Failure(Status.BadRequest, "Task has never been assigned to this roomie");

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
