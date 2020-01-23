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

        public async Task<Result<int>> AddItem(string itemName, float unitPrice)
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
                0 => Result.Success(p.Get<int>("@Status")),
                _ => Result.Failure<int>(Status.BadRequest, "Something went wrong")
            };
        }
        
        public async Task<int> DeleteItem(int itemId)
        {
            await using SqlConnection connection = new SqlConnection(ConnectionString);
            return await connection.ExecuteAsync(
                "Delete from rm2.tItem where ItemId = @itemId",
                new { ItemId = itemId});
        }
    }
}