using System;
using System.Threading.Tasks;
using NUnit.Framework;
using Roomies2.DAL.Gateways;
using Roomies2.DAL.Model.Grocery;
using Roomies2.DAL.Services;

namespace Roomies2.DAL.Tests.Tests
{
    [TestFixture]
    public class ItemGatewayTests
    {
        [Test]
        public async Task Can_Create_Update_Delete_Find_Item()
        {
            ItemGateway gateway = new ItemGateway(TestHelpers.ConnectionString);

            string itemName = TestHelpers.RandomTestName();
            int unitPrice = 1025;

            var itemResult = await gateway.AddItem(itemName, unitPrice);

            Assert.That(itemResult.Status, Is.EqualTo(Status.Created));

            int itemId = itemResult.Content;

            Result<Item> item;
            {
                item = await gateway.FindItemById(itemId);
                CheckItem(item, itemName, unitPrice);
            }
            {
                itemName = TestHelpers.RandomTestName();
                unitPrice = 525;

                await gateway.UpdateItem(itemId, itemName, unitPrice);

                item = await gateway.FindItemById(itemId);
                CheckItem(item, itemName, unitPrice);
            }
            {
                int row = await gateway.DeleteItem(itemId);
                Assert.That(row, Is.EqualTo(1));
                item = await gateway.FindItemById(itemId);
                Assert.That(item.Status, Is.EqualTo(Status.NotFound));
            }
        }

        private static void CheckItem(Result<Item> item, string itemName, int unitPrice)
        {
            Assert.That(item.HasError, Is.False);
            Assert.That(item.Status, Is.EqualTo(Status.Ok));
            Assert.That(item.Content.ItemName, Is.EqualTo(itemName));
            Assert.That(item.Content.UnitPrice, Is.EqualTo(unitPrice));
        }
    }
}