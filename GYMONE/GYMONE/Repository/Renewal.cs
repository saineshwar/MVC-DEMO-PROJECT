using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using GYMONE.Models;

namespace GYMONE.Repository
{
    public class Renewal : IRenewal
    {
        public RenewalDATA GetDataofMember(string MemberID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@MainMemberID", MemberID);
                return con.Query<RenewalDATA>("Usp_GetDataofMemberbyID", para, null, true, 0, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public string Get_PeriodID_byPlan(string PlanID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@PlanID", PlanID);
                return con.Query<string>("Usp_getPlanPeriodID", para, null, true, 0, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }


     
    }
}