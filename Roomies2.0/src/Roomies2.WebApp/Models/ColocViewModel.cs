

using System.ComponentModel.DataAnnotations;

namespace Roomies2.WebApp.Models
{
    public class ColocViewModel
    {
        [Required]
        public int RoomieId { get;  set; }
        [Required]
        public string ColocName { get;  set; }
    }
}
