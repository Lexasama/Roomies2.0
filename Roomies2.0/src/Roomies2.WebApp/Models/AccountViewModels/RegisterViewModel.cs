using System;
using System.ComponentModel.DataAnnotations;

namespace Roomies2.WebApp.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required]
        //[StringLength(8, ErrorMessage ="Invalid phone number.", MinimumLength =10)]
        //[RegularExpression(@"[0-9]", ErrorMessage ="Invalid phone number")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Required] [Display(Name = "Sex")] public int Sex { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Birthdate")]
        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
            MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}