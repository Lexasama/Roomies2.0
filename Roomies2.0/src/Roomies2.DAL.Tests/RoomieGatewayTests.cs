using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Roomies2.DAL.Gateways;
using Roomies2.DAL.Model.People;
using Roomies2.DAL.Services;

namespace Roomies2.DAL.Tests
{
    [TestFixture]
    internal class RoomieGatewayTests
    {
        private RoomieGateway Sut { get; }
        public RoomieGatewayTests() => Sut = new RoomieGateway(TestHelpers.ConnectionString);

        [Test]
        public async Task can_create_find_updateand_delete_roomie()
        {
            string lastName = TestHelpers.RandomTestName() ?? throw new ArgumentNullException(
                                  $"TestHelpers.RandomTestName()");
            string firstName = TestHelpers.RandomTestName() ?? throw new ArgumentNullException(
                                   $"TestHelpers.RandomTestName()");
            string phone = TestHelpers.RandomPhone();
            var sex = 0;
            var birthDate = TestHelpers.RandomBirthDate(20);
            var desc = "Une belle description";
            var pic = "Super photo";

            //Create
            var result = await Sut.Create(lastName, firstName, phone, sex, birthDate, desc, pic);
            
            Assert.That(result.Status, Is.EqualTo(Status.Created));

            int roomieId = result.Content;

            Result<RoomieData> roomie;
            {
                roomie = await Sut.FindById(roomieId);
                CheckRoomie(roomie, lastName, firstName, phone, sex, birthDate, desc, pic);
            }

            {
                lastName = TestHelpers.RandomTestName();
                firstName = TestHelpers.RandomTestName();
                await Sut.Update(lastName, firstName, phone, sex, birthDate, desc, pic);

                roomie = await Sut.FindById(roomieId);
                CheckRoomie(roomie, lastName, firstName, phone, sex, birthDate, desc, pic);
            }

            {
                var r = await Sut.Delete(roomieId);
                Assert.That(r.Status, Is.EqualTo(Status.Ok));
                roomie = await Sut.FindById(roomieId);
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