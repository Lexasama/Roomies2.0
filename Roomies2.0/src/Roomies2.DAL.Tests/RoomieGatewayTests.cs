using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Roomies2.DAL.Gateways;
using Roomies2.DAL.Model.People;
using Roomies2.DAL.Services;

namespace Roomies2.DAL.Tests
{
    [TestFixture]
    class RoomieGatewayTests
    {
        [Test]
        public async Task can_create_find_updateand_delete_roomie()
        {
            RoomieGateway sut = new RoomieGateway(TestHelpers.ConnectionString);

            string lastName = TestHelpers.RandomTestName();
            string firstName = TestHelpers.RandomTestName();
            string phone = TestHelpers.RandomPhone();
            int sex = 0;
            DateTime birthDate = TestHelpers.RandomBirthDate(20);
            string desc =  "unebelle description";
            string pic = "superphoto";

            //Create
            Result<int> result = await sut.Create(lastName, firstName, phone, sex, birthDate, desc, pic );
            Assert.That(result.Status, Is.EqualTo(Status.Created));

            int roomieId = result.Content;

            Result<RoomieData> roomie;
            {
                roomie = await sut.FindById(roomieId);
                CheckRoomie(roomie, lastName, firstName, phone,sex, birthDate, desc, pic);
            }

            {
                lastName = TestHelpers.RandomTestName();
                firstName = TestHelpers.RandomTestName();
                await sut.Update(lastName, firstName, phone, sex, birthDate, desc, pic);

                roomie = await sut.FindById(roomieId);
                CheckRoomie(roomie, lastName, firstName, phone, sex, birthDate, desc, pic);
            }

            {
                Result r = await sut.Delete(roomieId);
                Assert.That(r.Status, Is.EqualTo(Status.Ok));
                roomie = await sut.FindById(roomieId);
                Assert.That(roomie.Status, Is.EqualTo(Status.NotFound));
            }
            
        }

        public void CheckRoomie(Result<RoomieData> r, string lastName, string firstName, string phone, int sex, DateTime birthDate, string desc, string pic)
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
