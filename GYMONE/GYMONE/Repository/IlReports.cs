using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using GYMONE.Models;

namespace GYMONE.Repository
{
    interface IlReports
    {
        DataSet Generate_AllMemberDetailsReport();
        DataSet Get_MonthwisePayment_details(string MonthID);
        DataSet Get_YearwisePayment_details(string YearID);
        DataSet Get_RenewalReport(RenewalSearch objRS);
    }
}
