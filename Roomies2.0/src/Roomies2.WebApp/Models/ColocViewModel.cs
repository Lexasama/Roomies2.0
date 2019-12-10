

using System.ComponentModel.DataAnnotations;

namespace Roomies2.WebApp.Models
{
    public class ColocViewModel
    {
        public int ColocId { get; set; }
        public int RoomieId { get;  set; }
        [Required]
        public string ColocName { get;  set; }

        public string PicPath { get; set; }
    }
}
