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

        readonly Random _random;
            public TasksGatewayTests()
        {
            _random = new Random();
        }

        [Test]
        public async Task can_create_update_delete_a_task()
        {
            TasksGateway sut = new TasksGateway(TestHelpers.ConnectionString);

            string taskName = TestHelpers.RandomTestName();
            string des = TestHelpers.RandomTestName() + "descriptions";
            DateTime date = TestHelpers.RandomDate(5);
            int colocId = 1;
            bool state = false; // task done

            Result<int> taskResult = await sut.Create(taskName, des, date, colocId);
            Assert.That(taskResult.Status, Is.EqualTo(Status.Created));

            int taskId = taskResult.Content;
            Result<TaskData> task;

            {
                task = await sut.Find(taskId);
                CheckTask(task, taskName, des, date, colocId, state);
            }

            {
                taskName = TestHelpers.RandomTestName();
                date = TestHelpers.RandomDate(5);
                des = TestHelpers.RandomTestName() + "New DesCription";
                state = true;
                Result t = await sut.Update(taskId, taskName, date, des, state);
                Assert.That(t.Status, Is.EqualTo(Status.Ok));

                task = await sut.Find(taskId);
                CheckTask(task, taskName, des, date, colocId, state);

            }
            {
                
                Result s = await sut.UpdateState(taskId);
                Assert.That(s.Status, Is.EqualTo(Status.Ok));
                task = await sut.Find(taskId);
                CheckTask(task, taskName, des, date, colocId, !state );          
            }
            {
                Result result = await sut.Delete(taskId);
                Assert.That(result.Status, Is.EqualTo(Status.Ok));
                task = await sut.Find(taskId);
                Assert.That(task.Status, Is.EqualTo(Status.NotFound));
            }

        }

        [Test]
        public async Task can_assign_task()
        {
            TasksGateway sut = new TasksGateway(TestHelpers.ConnectionString);
            string taskName = TestHelpers.RandomTestName();
            string des = TestHelpers.RandomTestName() + "Description";
            int colocId = 1;
            DateTime date = TestHelpers.RandomDate(2);

            Result<int> taskResult = await sut.Create(taskName, des, date, colocId);
            Assert.That(taskResult.Status, Is.EqualTo(Status.Created));

            int taskId = taskResult.Content;
            Result<TaskData> task;

            {
                var r = await sut.Assign(taskId, 1);
                Assert.That(r.Status, Is.EqualTo(Status.Ok));
            }
            {
               var r = await sut.Assign(taskId, 1);
                Assert.That(r.Status, Is.EqualTo(Status.BadRequest));
            }
            {
                var r = await sut.Assign(taskId, 2);
                Assert.That(r.Status, Is.EqualTo(Status.Ok));
            }
            {
                var r = await sut.Unassign(taskId, 2);
                Assert.That(r.Status, Is.EqualTo(Status.Ok));
            }
            {
                var r = await sut.Unassign(taskId, 2);
                Assert.That(r.Status, Is.EqualTo(Status.BadRequest));
            }
        }

        public async void can_found_tasks_of_roomie()
        {

        }

        public async void can_find_roomies_of_task()
        {

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
