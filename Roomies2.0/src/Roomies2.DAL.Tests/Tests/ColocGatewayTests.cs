using System.Threading.Tasks;
using NUnit.Framework;
using Roomies2.DAL.Gateways;
using Roomies2.DAL.Model.BuildingManagement;
using Roomies2.DAL.Services;

namespace Roomies2.DAL.Tests.Tests
{
    [TestFixture]
    internal class ColocGatewayTests
    {
        public ColocGatewayTests()
        {
            Sut = new ColocGateway(TestHelpers.ConnectionString);
        }

        private ColocGateway Sut { get; }

        [Test]
        public async Task can_create_update_and_delete_a_coloc()
        {
            string name = TestHelpers.RandomTestName();

            ///create a roomie 
            var roomieResult = await TestStubs.StubRoomie();
            int roomieId = roomieResult.Content;

            var result = await Sut.Create(roomieId, name);
            Assert.That(result.Status, Is.EqualTo(Status.Created));

            int colocId = result.Content;

            Result<ColocData> colocData;
            {
                colocData = await Sut.FindById(colocId);
                CheckColoc(colocData, name);
            }

            {
                name = TestHelpers.RandomTestName();
                string picPath = "an other awesome picture ;)";
                Result r  = await Sut.Update(colocId, name, picPath);
                Assert.That(r.Status, Is.EqualTo(Status.Ok));

                colocData = await Sut.FindById(colocId);
                CheckColoc(colocData, name, picPath);
            }


            // Work on coloc deletion 
            //can delete a coloc only if there is one roomie left and has to delete all info such as Tasks, events, etc
            //{
            //    Result c = await Sut.Delete(colocId);
            //    Assert.That(c.Status, Is.EqualTo(Status.Ok));
            //    colocData = await Sut.FindById(colocId);
            //    Assert.That(c.Status, Is.EqualTo(Status.NotFound));
            //}
        }

        public void CheckColoc(Result<ColocData> c, string name)
        {
            Assert.That(c.HasError, Is.False);
            Assert.That(c.Status, Is.EqualTo(Status.Ok));
            Assert.That(c.Content.ColocName, Is.EqualTo(name));
        }
        public void CheckColoc(Result<ColocData> c, string name, string picPath)
        {
            Assert.That(c.HasError, Is.False);
            Assert.That(c.Status, Is.EqualTo(Status.Ok));
            Assert.That(c.Content.ColocName, Is.EqualTo(name));
            Assert.That(c.Content.PicPath, Is.EqualTo(picPath));
        }
    }
}