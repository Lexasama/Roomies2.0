using System;
using System.ComponentModel.DataAnnotations;

namespace Roomies2.WebApp.Models
{
    public class GroceryListModel
    {
        public int GroceryListId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        [Required]
        public int ColocId { get; set; }
        [Required]
        public int RoomieId { get; set; }
    }
}