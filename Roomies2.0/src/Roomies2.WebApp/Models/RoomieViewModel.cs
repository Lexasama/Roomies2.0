using System;
using System.ComponentModel.DataAnnotations;

namespace Roomies2.WebApp.Models.AccountViewModels
{
    public class RoomieViewModel
    {
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required]
        //[StringLength(8, ErrorMessage ="Invalid phone number.", MinimumLength =10)]
        [RegularExpression(@"[0-9]", ErrorMessage ="Invalid phone number")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        public string Description { get; set; }

        [Required]
        [Display(Name = "Sex")]
        public int Sex { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Birthdate")]
        public DateTime BirthDate { get; set; }
    }
}
