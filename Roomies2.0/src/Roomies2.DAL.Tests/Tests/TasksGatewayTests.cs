using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Roomies2.DAL.Gateways;
using Roomies2.DAL.Model.People;
using Roomies2.DAL.Services;

namespace Roomies2.DAL.Tests.Tests
{

    [TestFixture]
    class TasksGatewayTests
    {
        private TasksGateway Gateway { get; set; }

        public TasksGatewayTests()
        {
            Gateway = new TasksGateway(TestHelpers.ConnectionString);
        }

        [Test]
        public async Task can_create_update_delete_a_task()
        {
            string taskName = TestHelpers.RandomTestName();
            string des = TestHelpers.RandomTestName() + "descriptions";
            DateTime date = TestHelpers.RandomDate(5);
            int colocId = 1;

            var taskResult = await Gateway.Create(taskName, des, date, colocId);
            Assert.That(taskResult.Status, Is.EqualTo(Status.Created));

            int taskId = taskResult.Content;
            Result<TaskData> task;

            {
                task = await Gateway.Find(taskId);
                CheckTask(task, taskName, des, date, colocId, false);
            }

            {
                taskName = TestHelpers.RandomTestName();
                date = TestHelpers.RandomDate(5);
                des = TestHelpers.RandomTestName() + "New DesCription";
                Result t = await Gateway.Update(taskId, taskName, date, des, true);
                Assert.That(t.Status, Is.EqualTo(Status.Ok));

                task = await Gateway.Find(taskId);
                CheckTask(task, taskName, des, date, colocId, true);

            }
            {
                
                Result s = await Gateway.UpdateState(taskId);
                Assert.That(s.Status, Is.EqualTo(Status.Ok));
                task = await Gateway.Find(taskId);
                CheckTask(task, taskName, des, date, colocId, false );          
            }
            {
                Result result = await Gateway.Delete(taskId);
                Assert.That(result.Status, Is.EqualTo(Status.Ok));
                task = await Gateway.Find(taskId);
                Assert.That(task.Status, Is.EqualTo(Status.NotFound));
            }

        }

        [Test]
        public async Task can_assign_task()
        {
            string taskName = TestHelpers.RandomTestName();
            string des = TestHelpers.RandomTestName() + "Description";
            int colocId = 1;
            DateTime date = TestHelpers.RandomDate(2);

            //Create roomie
            Result<int> roomieResult = await TestStubs.StubRoomie();
            int roomieId = roomieResult.Content;


            var taskResult = await Gateway.Create(taskName, des, date, colocId);
            Assert.That(taskResult.Status, Is.EqualTo(Status.Created));

            int taskId = taskResult.Content;

            {
                Result r = await Gateway.Assign(taskId, roomieId);
                Assert.That(r.Status, Is.EqualTo(Status.Ok));
            }
            {
               Result r = await Gateway.Assign(taskId, roomieId);
                Assert.That(r.Status, Is.EqualTo(Status.BadRequest));
            }
            ///create an other roomie to update
            roomieResult = await TestStubs.StubRoomie();
            roomieId = roomieResult.Content;
            {
                Result r = await Gateway.Assign(taskId, roomieId);
                Assert.That(r.Status, Is.EqualTo(Status.Ok));
            }
            {
                Result r = await Gateway.Unassign(taskId, roomieId);
                Assert.That(r.Status, Is.EqualTo(Status.Ok));
            }
            {
                Result r = await Gateway.Unassign(taskId, roomieId);
                Assert.That(r.Status, Is.EqualTo(Status.BadRequest));
            }
        }
        [Test]
       public async Task test_return_value()
        {
            var r = await Gateway.GET(1);
            Console.WriteLine(r);
        }

        void CheckTask(Result<TaskData> task, string taskName, string des, DateTime date, int colocId, bool state)
        {
            Assert.That(task.Status, Is.EqualTo(Status.Ok));
            Assert.That(task.Content.TaskName, Is.EqualTo(taskName));
            Assert.That(task.Content.TaskDes, Is.EqualTo(des));
            Assert.That(task.Content.TaskDate, Is.EqualTo(date));
            Assert.That(task.Content.ColocId, Is.EqualTo(colocId));
            Assert.That(task.Content.State, Is.EqualTo(state));
        }
    }
}
