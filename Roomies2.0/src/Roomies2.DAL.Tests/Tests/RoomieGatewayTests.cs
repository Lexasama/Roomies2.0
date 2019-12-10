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
            UserGateway _userGateway = new UserGateway(TestHelpers.ConnectionString);

            string lastName = TestHelpers.RandomTestName();
            string firstName = TestHelpers.RandomTestName();
            string phone = TestHelpers.RandomPhone();
            string userName = TestHelpers.RandomTestName();
            var sex = 0;
            var birthDate = TestHelpers.RandomBirthDate(20);
            var desc = "Une belle description";
            string pic = null;
            string email = TestHelpers.RandomEmail();
            byte[] password = Guid.NewGuid().ToByteArray();


           Result<int> userResult = await _userGateway.CreatePasswordUser(userName, password);
           int roomieId = userResult.Content;
            
            //Create
            var result = await sut.Create(roomieId, userName, lastName, firstName, phone, sex, birthDate, desc, pic);

            Assert.That(result.Status, Is.EqualTo(Status.Created));

             roomieId = result.Content;

            Result<RoomieData> roomie;
            {
                roomie = await sut.FindById(roomieId);
                CheckRoomie(roomie, lastName, firstName, phone, sex, birthDate, desc, pic);
            }

            {
                lastName = TestHelpers.RandomTestName();
                firstName = TestHelpers.RandomTestName();
                email = TestHelpers.RandomEmail();
                await sut.Update(roomieId, userName, email, lastName, firstName, phone, sex, birthDate, desc, pic);

                roomie = await sut.FindById(roomieId);
                CheckRoomie(roomie, lastName, firstName, phone, sex, birthDate, desc, pic);
            }

            {
                var r = await sut.Delete(roomieId);
                Assert.That(r.Status, Is.EqualTo(Status.Ok));
                roomie = await sut.FindById(roomieId);
                Assert.That(roomie.Status, Is.EqualTo(Status.NotFound));
            }
        }

        private static void CheckRoomie(Result<RoomieData> r, string lastName, string firstName, string phone, int sex,
            DateTime birthDate, string desc, string pic)
        {
            Assert.That(r.HasError, Is.False);
            Assert.That(r.Status, Is.EqualTo(Status.Ok));
            Assert.That(r.Content.LastName, Is.EqualTo(lastName));
            Assert.That(r.Content.FirstName, Is.EqualTo(firstName));
            Assert.That(r.Content.BirthDate, Is.EqualTo(birthDate));
            Assert.That(r.Content.Phone, Is.EqualTo(phone));
            Assert.That(r.Content.Description, Is.EqualTo(desc));
            Assert.That(r.Content.PicPath, Is.EqualTo(pic));
            Assert.That(r.Content.Sex, Is.EqualTo(sex));
        }
    }
}