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
        public GroceriesGateway(string connectionString)
        {
            ConnectionString = connectionString;
        }

        private string ConnectionString { get; }

        public async Task<List<GroceryList>> GetAllGroceryListFromColocId(int colocId)
        {
            await using var con = new SqlConnection(ConnectionString);
            return await con.QueryAsync<GroceryList>("Select * from rm2.tGroceryList WHERE ColocId = @ColocId",
                new {ColocId = colocId}) as List<GroceryList>;
        }

        public async Task<Result<GroceryList>> GetGroceryListById(int groceryListId)
        {
            await using var con = new SqlConnection(ConnectionString);
            var list = await con.QueryFirstOrDefaultAsync<GroceryList>(
                "Select * from rm2.tGroceryList WHERE GroceryListId = @GroceryListId",
                new {GroceryListId = groceryListId});
            
            return list == null 
                ? Result.Failure<GroceryList>(Status.NotFound,"Item not found") 
                : Result.Success(Status.Ok, list);
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
                0 => Result.Success(Status.Created,p.Get<int>("@GroceryListId")),
                _ => Result.Failure<int>(Status.BadRequest, "Something went wrong")
            };
        }

        public async Task<Result<int>> UpdateGroceryList(int groceryListId, int roomieId, string name, DateTime dueDate)
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

        public async Task<Result> DeleteGroceryList(int groceryListId)
        {
            await using var con = new SqlConnection(ConnectionString);
            var p = new DynamicParameters();
            p.Add("@GroceryListId", groceryListId);
            p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            await con.ExecuteAsync("rm2.sGroceryListDelete", p, commandType: CommandType.StoredProcedure);
            var status = p.Get<int>("@Status");

            return status switch
            {
                0 => Result.Success(Status.Ok,status),
                _ => Result.Failure<int>(Status.BadRequest, "Grocery list doesn't exists")
            };
        }

        public async Task<List<Item>> GetAllItemsFromGroceryListId(int groceryListId)
        {
            await using var con = new SqlConnection(ConnectionString);
            return await con.QueryAsync<Item>(
                "Select * from rm2.itGroceryListItem where GroceryListId = @GroceryListId",
                new {GroceryListId = groceryListId}) as List<Item>;
        }

        public async Task<int> RemoveItemFromGroceryList(int groceryListId, int itemId)
        {
            await using var con = new SqlConnection(ConnectionString);
            return await con.ExecuteAsync(
                "Delete from rm2.itGroceryListItem where GroceryListId = @GroceryListId AND ItemId = @ItemId",
                new {GroceryListId = groceryListId, ItemId = itemId});
        }

        public async Task<Result> AddUpdateOrDeleteItemToGroceryList(int groceryListId, int itemId, int amount)
        {
            await using var con = new SqlConnection(ConnectionString);
            var p = new DynamicParameters();
            p.Add("@GroceryListId", groceryListId);
            p.Add("@ItemId", itemId);
            p.Add("@Amount", amount);
            p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            await con.ExecuteAsync("rm2.sGroceryListItemAddUpdateOrDelete", p, commandType: CommandType.StoredProcedure);
            var status = p.Get<int>("@Status");

            return status switch
            {
                0 => Result.Success(Status.Created),
                1 => Result.Success(Status.Ok),
                2 => Result.Success(Status.Ok),
                _ => Result.Failure(Status.BadRequest,"Amount is negative")
            };
        }
    }
}