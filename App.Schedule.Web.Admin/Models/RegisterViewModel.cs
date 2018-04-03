using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace App.Schedule.Web.Admin.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter your email id.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email id")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your password")]
        [MinLength(8, ErrorMessage = "Password must be greater than 8 character")]
        [MaxLength(50, ErrorMessage = "Password must be less than 50 character")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please enter confirm password")]
        [Compare("Password", ErrorMessage ="Please cheque your confirm password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Please enter first name.")]
        public string  FirstName { get; set; }
        [Required(ErrorMessage = "Please enter last name.")]
        public string LastName { get; set; }
    }
}