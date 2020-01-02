using Microsoft.Extensions.Configuration;
using Roomies2.DAL.Gateways;
using Roomies2.DAL.Model.People;
using Roomies2.DAL.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Roomies2.DAL.Tests
{
    public static class TestStubs
    {
        static IConfiguration _configuration;

        public static string ConnectionString
        {
            get 
            { 
                return Configuration["ConnectionStrings:Roomies2DB"]; 
            }
        }

        static IConfiguration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    _configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true)
                        .AddEnvironmentVariables()
                        .Build();
                }

                return _configuration;
            }
        }

        public static async Task<Result<int>> StubSupervisor()
        {
            SupervisorGateway supervisorGateway = new SupervisorGateway(ConnectionString);
            Result<int> user = await StubUser();

            string lastName = TestHelpers.RandomTestName();
            string firstName = TestHelpers.RandomTestName();
            string phone = TestHelpers.RandomPhone();

            Result<int> super = await supervisorGateway.Create(user.Content, lastName, firstName, phone );
            return super;
        }

        public static async Task<Result<int>> StubUser()
        {
            UserGateway sut = new UserGateway(ConnectionString);
            string email = $"user{Guid.NewGuid()}@test.com";
            Byte[] password = Guid.NewGuid().ToByteArray();

            Result<int> userResult = await sut.CreatePasswordUser(email, password);
            
            return userResult;
        }

        public static async Task<Result<int>> StubRoomie()
        {
            RoomieGateway rg = new RoomieGateway(ConnectionString);

            string lastName = TestHelpers.RandomTestName();
            string firstName = TestHelpers.RandomTestName();
            string phone = TestHelpers.RandomPhone();
            string userName = TestHelpers.RandomTestName();
            int sex = 0;
            DateTime birthDate = TestHelpers.RandomBirthDate(20);
            string desc = "Une belle description";
            string pic = "pic";

            Result<int> roomieId = await StubUser();

            Result<int> roomie = await rg.Create(roomieId.Content, userName, lastName, firstName, phone, sex, birthDate, desc, pic);

            return roomie;
        }
    }

    

}
