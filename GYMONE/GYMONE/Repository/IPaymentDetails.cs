using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GYMONE.Models;

namespace GYMONE.Repository
{
    interface IPaymentDetails
    {
        int InsertPaymentDetails(PaymentDetailsDTO objPD);
        int UpdatePaymentDetails(PaymentDetailsDTO objPD);
    }
}
