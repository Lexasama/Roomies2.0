using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Roomies2.DAL.Gateways;
using Roomies2.DAL.Model.BuildingManagement;
using Roomies2.DAL.Services;
namespace Roomies2.DAL.Tests
{
    [TestFixture]
    class ColocGatewayTests
    {
        readonly Random _random = new Random();

        public async Task can_create_update_and_delete_a_coloc()
        {
            ColocGateway sut = new ColocGateway(TestHelpers.ConnectionString);

            string name = TestHelpers.RandomTestName();
            string picPath = "awsome picture ;)";

            Result<int> result = await sut.Create(name, picPath);
            Assert.That(result.Status, Is.EqualTo(Status.Created));

            int colocId = result.Content;

            Result<ColocData> colocData;
            {
                colocData = await sut.FindById(colocId);
                CheckColoc(colocData, name, picPath);
            }

            {
                name = TestHelpers.RandomTestName();
                picPath = "an other awesome picture ;)";
                await sut.Update(name, picPath);

                colocData = await sut.FindById(colocId);
                CheckColoc(colocData, name, picPath);
            }

            {
                Result c = await sut.Delete(colocId);
                Assert.That(c.Status, Is.EqualTo(Status.Ok));
                colocData = await sut.FindById(colocId);
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
