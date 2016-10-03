using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using GYMONE.Models;
using System.Configuration;
using System.Data;

namespace GYMONE.Repository
{
    public class PlanMaster : IPlanMaster
    {
        public void InsertPlan(PlanMasterDTO Plan)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@PlanID", Plan.PlanID);
                paramater.Add("@SchemeID", Plan.SchemeID);
                paramater.Add("@PeriodID", Plan.PeriodID);
                paramater.Add("@PlanName", Plan.PlanName);
                paramater.Add("@PlanAmount", Plan.PlanAmount);
                paramater.Add("@ServiceTax", Plan.ServiceTax);
                paramater.Add("@CreateDate", Plan.CreateDate);
                paramater.Add("@CreateUserID", Plan.CreateUserID);
                paramater.Add("@ModifyDate", Plan.ModifyDate);
                paramater.Add("@ModifyUserID", Plan.ModifyUserID);
                paramater.Add("@RecStatus", Plan.RecStatus);
                var value = con.Query<int>("sprocPlanMasterInsertUpdateSingleItem", paramater, null, true, 0, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<PlanMasterDTO> GetPlan()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var ListofPlan = con.Query<PlanMasterDTO>("sprocPlanMasterSelectList", null, null, true, 0, commandType: CommandType.StoredProcedure);
                return ListofPlan;
            }
        }

        public PlanMasterDTO GetPlanByID(string PlanID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@PlanID", PlanID);
                var Plan_list = con.Query<PlanMasterDTO>("sprocPlanMasterSelectSingleItem", paramater, null, true, 0, CommandType.StoredProcedure).Single();
                return Plan_list;
            }
        }

        public void UpdatePlan(PlanMasterDTO Plan)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@PlanID", Plan.PlanID);
                paramater.Add("@SchemeID", Plan.SchemeID);
                paramater.Add("@PeriodID", Plan.PeriodID);
                paramater.Add("@PlanName", Plan.PlanName);
                paramater.Add("@PlanAmount", Plan.PlanAmount);
                paramater.Add("@ServiceTax", Plan.ServiceTax);
                paramater.Add("@CreateDate", Plan.CreateDate);
                paramater.Add("@CreateUserID", Plan.CreateUserID);
                paramater.Add("@ModifyDate", Plan.ModifyDate);
                paramater.Add("@ModifyUserID", Plan.ModifyUserID);
                paramater.Add("@RecStatus", Plan.RecStatus);
                var value = con.Query<int>("sprocPlanMasterInsertUpdateSingleItem", paramater, null, true, 0, commandType: CommandType.StoredProcedure);
            }

        }

        public void DeletePlan(string PlanID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@PlanID", PlanID); // Normal Parameters  
                var value = con.Query("sprocPlanMasterDeleteSingleItem", para, null, true, 0, CommandType.StoredProcedure);
            }
        }

        public bool PlannameExists(string Planname)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@Planmaster", Planname); // Normal Parameters  
                var value = con.Query<string>("Usp_checkplan", para, null, true, 0, CommandType.StoredProcedure).First();

                if (value == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public IEnumerable<PlanMasterDTO> GetPlanByWorkTypeID(string SchemeID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@SchemeID", SchemeID);
                var Plan_list = con.Query<PlanMasterDTO>("Usp_GetPlanByWorkTypeID", paramater, null, true, 0, CommandType.StoredProcedure);
                return Plan_list;
            }
        }
    }
}