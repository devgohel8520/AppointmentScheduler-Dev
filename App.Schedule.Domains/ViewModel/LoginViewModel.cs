using System.ComponentModel.DataAnnotations;

namespace App.Schedule.Domains.ViewModel
{
    /// <summary>
    /// Class is used to hold Login information.
    /// </summary>
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter your email id")]
        [EmailAddress(ErrorMessage = "Please enter a valid email id")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your password")]
        [MinLength(8, ErrorMessage = "Password must be greater than 8 character")]
        [MaxLength(50, ErrorMessage = "Password must be less than 50 character")]
        public string Password { get; set; }
        public bool IsKeepLoggedIn { get; set; }
    }
}
