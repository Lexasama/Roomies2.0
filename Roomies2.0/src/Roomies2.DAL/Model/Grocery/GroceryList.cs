#region

using System;
using System.Collections.Generic;

#endregion

namespace Roomies2.DAL.Model.Grocery
{
    public class GroceryList
    {
        public GroceryList(int groceryListId = default, string name = null, DateTime dateTime = default,
            List<Item> items = null)
        {
            GroceryListId = groceryListId;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            DateTime = dateTime;
            Items = items ?? throw new ArgumentNullException(nameof(items));
        }

        public int GroceryListId { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public List<Item> Items { get; set; }
    }
}