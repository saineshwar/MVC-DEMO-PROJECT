using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GYMONE.Models
{
    public class MemberRegistrationDTO
    {
        [Key]
        public int MemID { get; set; }
        public string MemberNo { get; set; }

        [DisplayName("First Name")]
        [Required(ErrorMessage = "Please enter First Name")]
        public string MemberFName { get; set; }
        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Please enter Last Name")]
        public string MemberLName { get; set; }
        [DisplayName("Middle Name")]
        [Required(ErrorMessage = "Please enter Middle Name")]
        public string MemberMName { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Birth Date")]
        [Required(ErrorMessage = "Please select Birth Date")]
        public DateTime? DOB { get; set; }


        [Required(ErrorMessage = "Please select Birth Date")]
        public string Age { get; set; }

        [Required(ErrorMessage = "Please enter Contactno")]
        public string Contactno { get; set; }

        [Required(ErrorMessage = "Please enter EmailID")]
        public string EmailID { get; set; }

        [validateGender(ErrorMessage = "Please select Gender")]
        public string Gender { get; set; }

        [DisplayName("Plan")]
        [ValidPlanAttribute(ErrorMessage = "Please select Plantype")]
        public int? PlantypeID { get; set; }

        [DisplayName("Workouttype")]
        [ValidWorkouttypeAttribute(ErrorMessage = "Please select Workouttype")]
        public int? WorkouttypeID { get; set; }


        public int? Createdby { get; set; }

        public int? ModifiedBy { get; set; }

        public string MemImagename { get; set; }

        public string MemImagePath { get; set; }

        [DisplayName("Joining Date")]
        [Required(ErrorMessage = "Please select Joining Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? JoiningDate { get; set; }

        [Required(ErrorMessage = "Please enter Address")]
        public string Address { get; set; }
        public int? MainMemberID { get; set; }
        [Required(ErrorMessage = "Amount Cannot be Empty")]
        public Decimal? Amount { get; set; }

        [NotMapped]
        public IEnumerable<SchemeMasterDTO> ListScheme { get; set; }
        [NotMapped]
        public IEnumerable<PlanMasterDTO> ListPlan { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> Listgender { get; set; }

        [NotMapped]
        public string FullName { get; set; }

        [NotMapped]
        public string PaymentID { get; set; }

        public class ValidWorkouttypeAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                if (Convert.ToInt32(value) == 0)
                    return false;
                else
                    return true;
            }


        }


        public class ValidPlanAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                if (Convert.ToInt32(value) == 0 || Convert.ToInt32(value) == null)
                    return false;
                else
                    return true;
            }
        }

        public class validateGender : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                if (Convert.ToInt32(value) == 0 || Convert.ToInt32(value) == null)
                    return false;
                else
                    return true;
            }
        }

    }

}