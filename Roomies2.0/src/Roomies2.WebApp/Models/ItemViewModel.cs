
using System.ComponentModel.DataAnnotations;

namespace Roomies2.WebApp.Models
{
    public class ItemViewModel
    {
        [Required]
        public string ItemName { get; set; }

        [Required]
        public int UnitPrice { get; set; }
    }
}
