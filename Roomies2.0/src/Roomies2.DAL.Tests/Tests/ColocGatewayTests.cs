using System.Threading.Tasks;
using NUnit.Framework;
using Roomies2.DAL.Gateways;
using Roomies2.DAL.Model.BuildingManagement;
using Roomies2.DAL.Services;

namespace Roomies2.DAL.Tests.Tests
{
    [TestFixture]
    class ColocGatewayTests
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
            string picPath = "awsome picture ;)";

            Result<int> result = await Sut.Create(name, picPath);
            Assert.That(result.Status, Is.EqualTo(Status.Created));

            int colocId = result.Content;

            Result<ColocData> colocData;
            {
                colocData = await Sut.FindById(colocId);
                CheckColoc(colocData, name, picPath);
            }

            {
                name = TestHelpers.RandomTestName();
                picPath = "an other awesome picture ;)";
                await Sut.Update(name, picPath);

                colocData = await Sut.FindById(colocId);
                CheckColoc(colocData, name, picPath);
            }

            {
                Result c = await Sut.Delete(colocId);
                Assert.That(c.Status, Is.EqualTo(Status.Ok));
                colocData = await Sut.FindById(colocId);
                Assert.That(c.Status, Is.EqualTo(Status.NotFound));
            }
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