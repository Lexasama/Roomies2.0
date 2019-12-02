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

        public async Task can_create_update_delete_a_task()
        {
            TasksGateway sut = new TasksGateway(TestHelpers.ConnectionString);

            string taskName = TestHelpers.RandomTestName();
            string des = TestHelpers.RandomTestName() + "descriptions";
            DateTime date = TestHelpers.RandomBirthDate(0);
            int colocId = 1;

            Result<int> taskResult = await sut.Create(taskName, des, date, colocId);
            Assert.That(taskResult.Status, Is.EqualTo(Status.Created));

            int taskId = taskResult.Content;
            Result<TaskData> task;

            {
                task = await sut.Find(taskId);
                CheckTask(task, taskName, des, date, colocId);
            }

            {
                taskName = TestHelpers.RandomTestName();
                date =  DateTime.Now;
                des = TestHelpers.RandomTestName() + "New DesCription";

                Result t = await sut.Update(taskName, date, des);
                Assert.That(t.Status, Is.EqualTo(Status.Ok));

                task = await sut.Find(taskId);
                CheckTask(task, taskName, des, date, colocId);

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
        }

        void CheckTask(Result<TaskData> task, string taskName, string des, DateTime date, int colocId)
        {
            Assert.That(task.Status, Is.EqualTo(Status.Ok));
            Assert.That(task.Content.TaskName, Is.EqualTo(taskName));
            Assert.That(task.Content.Description, Is.EqualTo(des));
            Assert.That(task.Content.Date, Is.EqualTo(date));
            Assert.That(task.Content.ColocId, Is.EqualTo(colocId));


        }
    }
}
