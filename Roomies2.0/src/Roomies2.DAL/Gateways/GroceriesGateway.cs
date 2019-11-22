using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Roomies2.DAL.Model.Grocery;
using Roomies2.DAL.Services;

namespace Roomies2.DAL.Gateways
{
    public class GroceriesGateway
    {
        public GroceriesGateway(string connectionString) => ConnectionString = connectionString;

        private string ConnectionString { get; }

        public async Task<List<GroceryList>> GetAllGroceryListFromColocId(int colocId)
        {
            await using var con = new SqlConnection(ConnectionString);
            return await con.QueryAsync<GroceryList>("Select * from rm2.tGroceryList WHERE ColocId = @ColocId",
                new {ColocId = colocId}) as List<GroceryList>;
        }

        

        public async Task<Result<int>> CreateGroceryList(int colocId, int roomieId, string name, DateTime dueDate)
        {
            await using var con = new SqlConnection(ConnectionString);
            var p = new DynamicParameters();
            p.Add("@ColocId", colocId);
            p.Add("@RoomieId", roomieId);
            p.Add("@Name", name);
            p.Add("@DueDate", dueDate);
            p.Add("@GroceryListId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            await con.ExecuteAsync("rm2.sGroceryListCreate", p, commandType: CommandType.StoredProcedure);

            var status = p.Get<int>("@Status");

            return status switch
            {
                0 => Result.Success(p.Get<int>("@Status")),
                _ => Result.Failure<int>(Status.BadRequest, "Something went wrong")
            };
        }

        public async Task<Result<int>> UpdateGroceryList(int groceryListId,int roomieId, string name, DateTime dueDate)
        {
            await using var con = new SqlConnection(ConnectionString);
            var p = new DynamicParameters();
            p.Add("@GroceryListId", groceryListId);
            p.Add("@RoomieId", roomieId);
            p.Add("@Name", name);
            p.Add("@DueDate", dueDate);
            p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            await con.ExecuteAsync("rm2.sGroceryListUpdate", p, commandType: CommandType.StoredProcedure);

            var status = p.Get<int>("@Status");

            return status switch
            {
                0 => Result.Success(p.Get<int>("@Status")),
                _ => Result.Failure<int>(Status.BadRequest, "Grocery list doesn't exists")
            };
        }

        public async Task<Result<int>> DeleteGroceryList(int groceryListId)
        {
            await using var con = new SqlConnection(ConnectionString);
            var p = new DynamicParameters();
            p.Add("@GroceryListId", groceryListId);
            p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            await con.ExecuteAsync("rm2.sCreateGroceryList", p, commandType: CommandType.StoredProcedure);
            var status = p.Get<int>("@Status");

            return status switch
            {
                0 => Result.Success(p.Get<int>("@Status")),
                _ => Result.Failure<int>(Status.BadRequest, "Grocery list doesn't exists")
            };
        }
    }
}