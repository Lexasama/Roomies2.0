#region

using System;

#endregion

namespace Roomies2.DAL.Model.Grocery
{
    public class Item
    {
        public Item(int itemId = default, string itemName = null, double price = default, int quantity = default)
        {
            ItemId = itemId;
            ItemName = itemName ?? throw new ArgumentNullException(nameof(itemName));
            Price = price;
            Quantity = quantity;
        }

        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}