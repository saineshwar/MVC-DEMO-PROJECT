using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace GYMONE.Models
{
    public class PaymentlistingDTO
    {
        public int MemberID { get; set; }
        public int PaymentID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Contactno { get; set; }
        public string EmailID { get; set; }
        public int MemberNo { get; set; }
        public string PlanName { get; set; }
        public string SchemeName { get; set; }
        public string JoiningDate { get; set; }
        public string RenwalDate { get; set; }
        public string PaymentAmount { get; set; }
        public string WorkouttypeID { get; set; }
        public string PlantypeID { get; set; }
        public string MemberReg { get; set; }
        
        public IEnumerable<PaymentlistingDTO> PaymentList { get; set; }
    }

    public class PaymentAutocompDTO
    {
        public string MemberNo { get; set; }
        public int MainMemberID { get; set; }
        public string Name { get; set; }
    }


    public class PaymentlistingDTOVM
    {
        public int? Page { get; set; }
        public IPagedList<PaymentlistingDTO> SearchResults { get; set; }
        public string SearchButton { get; set; }
        public string PaymentSearch { get; set; }
        public int? Memberno { get; set; }
        public string MemberName { get; set; }


    }
}