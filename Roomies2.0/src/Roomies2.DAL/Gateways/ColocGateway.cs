using Roomies2.DAL.Services;
using System.Threading.Tasks;
using System;
using Roomies2.DAL.Model.BuildingManagement;

namespace Roomies2.DAL.Gateways
{
    public class ColocGateway
    {
        public string ConnectionString { get; }

        public ColocGateway(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public Task<Result<ColocData>> FindById(int colocId)
        {
            throw new NotImplementedException();
        }

        public Result<int> Create(string name, string picPath)
        {
            if (!IsNameValid(name)) return Result.Failure<int>(Status.BadRequest, "The name is not valid");
            throw new NotImplementedException();
        }

        public Task<Result> Delete(int colocId)
        {
            throw new NotImplementedException();
        }

        public Result<int> Update(string name, string picPath)
        {
            if (!IsNameValid(name)) return Result.Failure<int>(Status.BadRequest, "The Name is not valid");
            throw new NotImplementedException();
        }

        bool IsNameValid(string name) => string.IsNullOrWhiteSpace(name);
    }
}