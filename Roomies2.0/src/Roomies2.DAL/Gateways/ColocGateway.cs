using Roomies2.DAL.Services;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using Dapper;
using System;
using Roomies2.DAL.Model.BuildingManagement;

namespace Roomies2.DAL.Gateways
{
    public class ColocGateway
    {
        readonly string _connectionString;

        public ColocGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Task<Result<ColocData>> FindById(int colocId)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<int>> Create(string name, string picPath)
        {
            if (!IsNameValid(name)) return Result.Failure<int>(Status.BadRequest, "The name is not valid");
            throw new NotImplementedException();
        }

        public Task<Result> Delete(int colocId)
        {
            throw new NotImplementedException();
        }

        public async  Task<Result> Update(string name, string picPath)
        {
            if (!IsNameValid(name)) return Result.Failure<int>(Status.BadRequest, "The Name is not valid");
            throw new NotImplementedException();
        }

        bool IsNameValid(string name) => string.IsNullOrWhiteSpace(name);
    }
}
