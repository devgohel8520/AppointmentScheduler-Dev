using System.ComponentModel.DataAnnotations;

namespace App.Schedule.Web.Admin.Models
{
    public class ForgotViewModel
    {
        [Required(ErrorMessage = "Please enter your email id")]
        public string Email { get; set; }
    }
}