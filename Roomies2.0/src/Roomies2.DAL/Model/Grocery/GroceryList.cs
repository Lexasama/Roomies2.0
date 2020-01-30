#region

using System;

#endregion

namespace Roomies2.DAL.Model.Grocery
{
    public class GroceryList
    { 
        public int GroceryListId { get; set; }
        public int ColocId { get; set; }
        public int RoomieId { get; set; }
        public string ListName { get; set; }
        public DateTime DueDate { get; set; }
    }
}