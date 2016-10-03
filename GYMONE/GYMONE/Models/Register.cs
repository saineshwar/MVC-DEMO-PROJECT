using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GYMONE.Models
{
    public class Register
    {
        [Required(ErrorMessage = "Enter Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Enter Username")]
        [Remote("CheckUserNameExists", "Account", ErrorMessage = "Username already exists!")]
        public string username { get; set; }

        [Required(ErrorMessage = "Enter EmailID")]
        public string EmailID { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }
        

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("password", ErrorMessage = "The password and confirmation password do not match.")]
        public string Confirmpassword { get; set; }


       
    }
}