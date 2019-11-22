using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Roomies2.DAL.Model.Grocery;
using Roomies2.DAL.Services;

namespace Roomies2.DAL.Gateways
{
    public class ItemGateway
    {
        public ItemGateway(string connectionString) => ConnectionString = connectionString;
        private string ConnectionString { get; }

        public async Task<List<Item>> GetAllItemsFromGroceryListId(int groceryListId)
        {
            await using var con = new SqlConnection(ConnectionString);
            return await con.QueryAsync<Item>(
                "Select * from rm2.itGroceryListItem where GroceryListId = @GroceryListId",
                new {GroceryListId = groceryListId}) as List<Item>;
        }

        public async Task<Result<int>> RemoveItemFromGroceryList(int groceryListId)
        {
            await using var con = new SqlConnection(ConnectionString);
            var p = new DynamicParameters();
            p.Add("@GroceryListId", groceryListId);
            p.Add("@Status", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);

            await con.ExecuteAsync("sDeleteItemFromGroceryList", p, commandType: CommandType.StoredProcedure);

            var status = p.Get<int>("@Status");
            return status switch
            {
                0 => Result.Success(status),
                _ => Result.Failure<int>(Status.BadRequest, "Item doesn't exists")
            };
        }

        public async Task AddItemToGroceryList(int groceryListId, List<Item> items)
        {
            await using var con = new SqlConnection(ConnectionString);

            foreach (var item in items)
            {
                var p = new DynamicParameters();
                p.Add("@GroceryListId", groceryListId);
                p.Add("@ItemId", item.ItemId);
                await con.ExecuteAsync("sAddItemToGroceryList", p, commandType: CommandType.StoredProcedure);
            }
        }
        
        
    }
}