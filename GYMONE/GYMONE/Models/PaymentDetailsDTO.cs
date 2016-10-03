using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GYMONE.Models
{
    public class PaymentDetailsDTO
    {

        public Int64 PaymentID { get; set; }
        public Int32 PlanID { get; set; }
        public Int32 WorkouttypeID { get; set; }
        public String Paymenttype { get; set; }
        public DateTime PaymentFromdt { get; set; }
        public DateTime PaymentTodt { get; set; }
        public Decimal PaymentAmount { get; set; }
        public DateTime NextRenwalDate { get; set; }
        public DateTime CreateDate { get; set; }
        public Int32 CreateUserID { get; set; }
        public DateTime ModifyDate { get; set; }
        public Int32 ModifyUserID { get; set; }
        public String RecStatus { get; set; }
        public Int64 MemberID { get; set; }
        public String MemberNo { get; set; }

    }
}