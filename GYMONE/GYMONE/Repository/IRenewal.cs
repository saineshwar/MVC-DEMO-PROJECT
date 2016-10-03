using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GYMONE.Models;

namespace GYMONE.Repository
{
    interface IRenewal
    {
        RenewalDATA GetDataofMember(string MemberID);
        string Get_PeriodID_byPlan(string PlanID);
    }
}
