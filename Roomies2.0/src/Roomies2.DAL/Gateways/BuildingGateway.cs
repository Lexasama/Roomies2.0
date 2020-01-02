using Roomies2.DAL.Model.BuildingManagement;
using Roomies2.DAL.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Roomies2.DAL.Gateways
{
    public class BuildingGateway
    {
        readonly string _connectionString;

        public BuildingGateway(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Result<int>> Create(string name, string addresse)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<BuildingData>> Find(int buildingId)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> Update(int buildingId, string name, string addresse)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> Delete(int buildingId)
        {
            throw new NotImplementedException();
        }
    }
}
