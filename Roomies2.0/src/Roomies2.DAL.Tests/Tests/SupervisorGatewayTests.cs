using NUnit.Framework;
using Roomies2.DAL.Gateways;
using Roomies2.DAL.Model.People;
using Roomies2.DAL.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Roomies2.DAL.Tests.Tests
{
    [TestFixture]
    class SupervisorGatewayTests
    {
        [Test]
        public async Task can_create_edit_update_and_delete_a_supervisor()
        {
            SupervisorGateway sut = new SupervisorGateway(TestHelpers.ConnectionString);
            UserGateway userGateway = new UserGateway(TestHelpers.ConnectionString);

            string lastName = TestHelpers.RandomTestName();
            string firstName = TestHelpers.RandomTestName();
            string phone = TestHelpers.RandomPhone();
            string email = TestHelpers.RandomEmail();
            var password = Guid.NewGuid().ToByteArray();


            var userResult = await userGateway.CreatePasswordUser(email, password);

            //Create
            var result = await sut.Create(lastName, firstName, phone);

            Assert.That(result.Status, Is.EqualTo(Status.Created));

            int superId = result.Content;

            Result<SupervisorData> supervisor;
            {
                supervisor = await sut.Find(superId);
                CheckSupervisor(supervisor, lastName, firstName, phone, email);
            }
            {
                lastName = TestHelpers.RandomTestName();
                firstName = TestHelpers.RandomTestName();
                email = TestHelpers.RandomEmail();

                await sut.Update(superId, lastName, firstName, email, phone);

                supervisor = await sut.Find(superId);
                CheckSupervisor(supervisor, lastName, firstName, phone, email);
            }
            {
                Result r = await sut.Delete(superId);
                Assert.That(r.Status, Is.EqualTo(Status.Ok));
                supervisor = await sut.Find(superId);
                Assert.That(supervisor.Status, Is.EqualTo(Status.NotFound)));
            }
        }

        private static void CheckSupervisor(Result<SupervisorData> s, string lastname, string firstName, string phone, string email)
        {
            Assert.That(s.HasError, Is.False);
            Assert.That(s.Status, Is.EqualTo(Status.Ok));
            Assert.That(s.Content.LastName, Is.EqualTo(lastname));
        }
            
    }
}
