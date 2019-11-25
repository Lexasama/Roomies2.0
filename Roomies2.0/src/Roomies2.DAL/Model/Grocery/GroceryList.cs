#region

using System;

#endregion

namespace Roomies2.DAL.Model.Grocery
{
    public class GroceryList
    {
        public GroceryList(int colocId, int roomieId, int groceryListId = default, string name = null,
            DateTime dateTime = default)
        {
            ColocId = colocId;
            RoomieId = roomieId;
            GroceryListId = groceryListId;
            ListName = name ?? throw new ArgumentNullException(nameof(name));
            DueDate = dateTime;
        }

        public int GroceryListId { get; set; }
        public int ColocId { get; set; }
        public int RoomieId { get; set; }
        public string ListName { get; set; }
        public DateTime DueDate { get; set; }
    }
}