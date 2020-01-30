using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Roomies2.DAL.Gateways;
using Roomies2.DAL.Model.Grocery;
using Roomies2.DAL.Services;

namespace Roomies2.DAL.Tests.Tests
{
    [TestFixture]
    public class GroceriesGatewayTests
    {
        [Test]
        public async Task Can_Create_Find_Update_Delete_GroceryList()
        {
            GroceriesGateway gateway = new GroceriesGateway(TestHelpers.ConnectionString);

            string listName = TestHelpers.RandomTestName();
            DateTime dateTime = TestHelpers.RandomBirthDate(0);
            int colocId = 0;
            int roomieId = 0;

            var listResult = await gateway.CreateGroceryList(colocId, roomieId, listName, dateTime);

            Assert.That(listResult.Status, Is.EqualTo(Status.Created));

            int listId = listResult.Content;

            Result<GroceryList> groceryList;
            {
                groceryList = await gateway.GetGroceryListById(listId);
                CheckList(groceryList, listName, colocId, roomieId, dateTime);
            }
            {
                listName = TestHelpers.RandomTestName();
                dateTime = TestHelpers.RandomBirthDate(0);

                await gateway.UpdateGroceryList(listId, roomieId, listName, dateTime);

                groceryList = await gateway.GetGroceryListById(listId);
                CheckList(groceryList, listName, colocId, roomieId, dateTime);
            }
            {
                var status = await gateway.DeleteGroceryList(listId);
                Assert.That(status.Status, Is.EqualTo(Status.Ok));
                groceryList = await gateway.GetGroceryListById(listId);
                Assert.That(groceryList.Status, Is.EqualTo(Status.NotFound));
            }
        }

        private static void CheckList(
            Result<GroceryList> list, string listName, int colocId, int roomieId, DateTime dueDate)
        {
            Assert.That(list.HasError, Is.False);
            Assert.That(list.Status, Is.EqualTo(Status.Ok));
            Assert.That(list.Content.ListName, Is.EqualTo(listName));
            Assert.That(list.Content.ColocId, Is.EqualTo(colocId));
            Assert.That(list.Content.RoomieId, Is.EqualTo(roomieId));
            Assert.That(list.Content.DueDate, Is.EqualTo(dueDate));
        }
    }
}

