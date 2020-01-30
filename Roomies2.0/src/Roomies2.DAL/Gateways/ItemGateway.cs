using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using Dapper;
using Roomies2.DAL.Model.Grocery;
using Roomies2.DAL.Services;

namespace Roomies2.DAL.Gateways
{
    public class ItemGateway
    {
        public ItemGateway(string connectionString)
        {
            ConnectionString = connectionString;
        }

        private string ConnectionString { get; }


        public async Task<List<Item>> GetAllItems()
        {
            await using var connection = new SqlConnection(ConnectionString);
            return (List<Item>) await connection.QueryAsync<Item>("SELECT * FROM rm2.tItem WHERE ItemId <> 0");
        }

        public async Task<Result<Item>> FindItemById(int id)
        {
            await using var connection = new SqlConnection(ConnectionString);
            
            var item = await connection.QueryFirstOrDefaultAsync<Item>("SELECT * FROM rm2.tItem WHERE ItemId = @itemId",
                new {itemId = id});
            
            return item == null 
                ? Result.Failure<Item>(Status.NotFound,"Item not found") 
                : Result.Success(Status.Ok, item);
        }
        
        public async Task<Result> UpdateItem(int itemId, string itemName, int unitPrice)
        {
            await using var con = new SqlConnection(ConnectionString);
            var p = new DynamicParameters();
            p.Add("@itemId", itemId);
            p.Add("@itemName", itemName);
            p.Add("@unitPrice", unitPrice);
            p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            await con.ExecuteAsync("rm2.sItemUpdate", p, commandType: CommandType.StoredProcedure);

            var status = p.Get<int>("@Status");
                
            return status == 1 
                ? Result.Failure<int>(Status.NotFound, "Not found") 
                : Result.Success();
        }

        public async Task<Result> IncreaseQuantity(int itemId)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                await con.ExecuteAsync(
                    @"UPDATE rm2.itGroceryListItem SET ItemAmount= ItemAmount + 1 WHERE ItemId = @ItemId and ItemAmount > 0;", 
                new { ItemId = itemId});
            }
            return Result.Success(Status.Ok);
        }

        public async Task<Result> DecreaseQuantity(int itemId)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                await con.ExecuteAsync(
                    @"UPDATE rm2.itGroceryListItem SET ItemAmount= ItemAmount - 1 WHERE ItemId = @ItemId and ItemAmount > 0;",
                new { ItemId = itemId });
            }
            return Result.Success(Status.Ok);
        }

        public async Task<Result<int>> AddItem(string itemName, int unitPrice)
        {
            await using var con = new SqlConnection(ConnectionString);
            var p = new DynamicParameters();
            p.Add("@Name", itemName);
            p.Add("@Price", unitPrice);
            p.Add("@ItemId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            await con.ExecuteAsync("rm2.sItemCreate", p, commandType: CommandType.StoredProcedure);

            var status = p.Get<int>("@Status");

            return status switch
            {
                0 => Result.Success(Status.Created,p.Get<int>("@ItemId")),
                _ => Result.Failure<int>(Status.BadRequest, "Something went wrong")
            };
        }
        
        public async Task<int> DeleteItem(int itemId)
        {
            await using SqlConnection connection = new SqlConnection(ConnectionString);
            return await connection.ExecuteAsync(
                @"DELETE FROM rm2.itGroceryListItem WHERE ItemId = @id
                    Delete from rm2.tItem where ItemId = @id",
                new { id = itemId});
        }
    }
}