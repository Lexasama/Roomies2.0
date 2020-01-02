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
            get { return Configuration["ConnectionStrings:Roomies2DB"]; }
        }

        static IConfiguration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    _configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsetting.json", optional: false)
                        .AddEnvironmentVariables()
                        .Build();
                }

                return _configuration;
            }
        }

        public static async Task<int> StubUserId()
        {
            UserGateway sut = new UserGateway(ConnectionString);
            string email = $"user{Guid.NewGuid()}@test.com";
            Byte[] password = Guid.NewGuid().ToByteArray();

            var c = await sut.CreatePasswordUser(email, password);
            
            return c.Content;
        }

        public static async Task<int> StubRoomieId()
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

            int roomieId = await StubUserId();

            var r = await rg.Create(roomieId, userName, lastName, firstName, phone, sex, birthDate, desc, pic);

            return r.Content;
        }
    }

    

}
