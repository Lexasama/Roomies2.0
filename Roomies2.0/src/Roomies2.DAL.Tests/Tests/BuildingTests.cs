using NUnit.Framework;
using Roomies2.DAL.Gateways;
using Roomies2.DAL.Model.BuildingManagement;
using Roomies2.DAL.Services;
using System;
using System.Threading.Tasks;

namespace Roomies2.DAL.Tests.Tests
{
    [TestFixture]
    class BuildingTests
    {
        [Test]
        public async Task can_create_find_update_and_delete_a_building()
        {
            BuildingGateway sut = new BuildingGateway(TestHelpers.ConnectionString);
            string addresse = "randomaddresse" + Guid.NewGuid().ToString();
            string name = TestHelpers.RandomTestName();

           var supervisor = await TestStubs.StubSupervisor();
            int supervisorId = supervisor.Content;

            Console.WriteLine(supervisorId);


            Result<int> result = await sut.Create(name, addresse, supervisorId);
            Assert.That(result.Status, Is.EqualTo(Status.Created));
            int buildingId = result.Content;

            Result<BuildingData> building;
            {
                building = await sut.Find(buildingId);
                CheckBuilding(building, name, addresse);
            }
            {
               addresse = "randomaddresse" + Guid.NewGuid().ToString();
               name = TestHelpers.RandomTestName();
               await sut.Update(buildingId, name, addresse);

                building = await sut.Find(buildingId);
                CheckBuilding(building, name, addresse);
            }

            {
                Result r = await sut.Delete(buildingId);
                Assert.That(r.Status, Is.EqualTo(Status.Ok));
                building = await sut.Find(buildingId);
                Assert.That(building.Status, Is.EqualTo(Status.NotFound));
            }

        }

        private static void CheckBuilding(Result<BuildingData> building, string name, string adresse)
        {

            Assert.That(building.HasError, Is.False);
            Assert.That(building.Status, Is.EqualTo(Status.Ok));
            Assert.That(building.Content.BuildingName, Is.EqualTo(name));
            Assert.That(building.Content.BuildingAddress, Is.EqualTo(adresse));
        }
    }
}
