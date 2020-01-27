using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Roomies2.DAL.Gateways;
using Roomies2.DAL.Model.People;
using Roomies2.DAL.Services;

namespace Roomies2.DAL.Tests.Tests
{
    [TestFixture]
    internal class RoomieGatewayTests
    {
         
       
        [Test]
        public async Task can_create_find_update_and_delete_roomie()
        {
            RoomieGateway sut = new RoomieGateway(TestHelpers.ConnectionString);
            UserGateway userGateway = new UserGateway(TestHelpers.ConnectionString);

            string lastName = TestHelpers.RandomTestName();
            string firstName = TestHelpers.RandomTestName();
            string phone = TestHelpers.RandomPhone();
            string userName = TestHelpers.RandomTestName();
            int sex = 0;
            DateTime birthDate = TestHelpers.RandomBirthDate(20);
            string desc = "Une belle description";
            var password = Guid.NewGuid().ToByteArray();


           var userResult = await userGateway.CreatePasswordUser(userName, password);
           int roomieId = userResult.Content;
            
            //Create
            var result = await sut.Create(roomieId, userName, lastName, firstName, phone, sex, birthDate, desc, null);

            Assert.That(result.Status, Is.EqualTo(Status.Created));

             roomieId = result.Content;

            Result<RoomieData> roomie;
            {
                roomie = await sut.FindById(roomieId);
                CheckRoomie(roomie, lastName, firstName, phone, sex, birthDate, desc, null);
            }

            {
                lastName = TestHelpers.RandomTestName();
                firstName = TestHelpers.RandomTestName();
                string email = TestHelpers.RandomEmail();
                await sut.Update(roomieId, userName, email, lastName, firstName, phone, sex, birthDate, desc, null);

                roomie = await sut.FindById(roomieId);
                CheckRoomie(roomie, lastName, firstName, phone, sex, birthDate, desc, null);
            }

            {
                Result r = await sut.Delete(roomieId);
                Assert.That(r.Status, Is.EqualTo(Status.Ok));
                roomie = await sut.FindById(roomieId);
                Assert.That(roomie.Status, Is.EqualTo(Status.NotFound));
            }
        }

        private static void CheckRoomie(Result<RoomieData> roomie, string lastName, string firstName, string phone, in int sex, in DateTime birthDate, string desc, object p7)
        {
            throw new NotImplementedException();
        }
    }
}