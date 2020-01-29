#region

using System;

#endregion

namespace Roomies2.DAL.Model.Grocery
{
    public class Item
    {
        public Item()
        {
            
        }

        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int UnitPrice { get; set; }
    }
}