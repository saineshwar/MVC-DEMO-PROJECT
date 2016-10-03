using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GYMONE.Models
{
    public class RenewalDTO
    {
        public string SearchButton { get; set; }
        public string PaymentSearch { get; set; }
        public string MembernoSearch { get; set; }
        public string MemberName { get; set; }

        public RenewalDATA RenewalDATA { get; set; }
    }

    public class RenewalDATA
    {
        public string PaymentAmount { get; set; }
        public string RenwalDate { get; set; }
        public string NewDate { get; set; }
        public string ContinuesDate { get; set; }
        public string Name { get; set; }
        public string MemberNo { get; set; }
        public string MainMemberID { get; set; }
        public string WorkouttypeID { get; set; }
        public string PlantypeID { get; set; }
        public string DrpRenewal { get; set; }
        public string MemberID { get; set; }
        public string PaymentID { get; set; }
        [NotMapped]
        public IEnumerable<SchemeMasterDTO> ListScheme { get; set; }
        [NotMapped]
        public IEnumerable<PlanMasterDTO> ListPlan { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> ListRenewaltype { get; set; }
    }

    

}