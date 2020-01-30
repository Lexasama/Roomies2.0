using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Roomies2.WebApp.Models
{
    public class GroceryListItemsViewModel
    {
        [Required]
        public IEnumerable<int> ItemIdList { get; set; }

        [Required]
        public int GroceryListId { get; set; }
    }
}
