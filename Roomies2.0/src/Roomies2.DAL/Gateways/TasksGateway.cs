using Roomies2.DAL.Model.People;
using Roomies2.DAL.Services;
using System;
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
            throw new NotImplementedException();
        }

        public Task<Result<int>> Create(string taskName, string des, DateTime date, int colocId)
        {
            throw new NotImplementedException();
        }
    }
}
