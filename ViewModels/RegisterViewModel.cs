using System.ComponentModel.DataAnnotations;

namespace HospitalApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required]    //Required field
        [Display(Name = "Username")]
        public string? UserName { get; set; }

        [Required]   //Required field
        [EmailAddress]   // will be in email concept
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required]    //Required field
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; }

        [Required]   //Required field
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}