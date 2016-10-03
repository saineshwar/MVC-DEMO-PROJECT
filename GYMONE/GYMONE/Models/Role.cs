using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GYMONE.Models
{
    public class Role
    {
        [NotMapped]
        public int RoleId { get; set; }

        [Required (ErrorMessage="Enter Role name")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string RoleName { get; set; }
    }
}