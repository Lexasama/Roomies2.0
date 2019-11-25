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

        
    }
}