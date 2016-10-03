using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using GYMONE.Models;

namespace GYMONE.Repository
{
    public class ReportsMaster : IlReports
    {
        public DataSet Generate_AllMemberDetailsReport()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                con.Open();
                DataSet ds = new DataSet();

                try
                {
                    SqlCommand cmd = new SqlCommand("Usp_GetAllRenwalrecords", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);

                    if (ds.Tables.Count > 0)
                    {
                        return ds;
                    }

                    else
                    {
                        return ds = null;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    ds.Dispose();
                }

            }
        }

        public DataSet Get_MonthwisePayment_details(string MonthID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                con.Open();
                DataSet ds = new DataSet();

                try
                {
                    SqlCommand cmd = new SqlCommand("Usp_GetMonthwisepaymentdetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@month", MonthID);
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);

                    if (ds.Tables.Count > 0)
                    {
                        return ds;
                    }

                    else
                    {
                        return ds = null;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    ds.Dispose();
                }

            }
        }

        public DataSet Get_YearwisePayment_details(string YearID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                con.Open();
                DataSet ds = new DataSet();

                try
                {
                    SqlCommand cmd = new SqlCommand("Usp_GetYearwisepaymentdetails", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@year", YearID);
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);

                    if (ds.Tables.Count > 0)
                    {
                        return ds;
                    }

                    else
                    {
                        return ds = null;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    ds.Dispose();
                }

            }
        }

        public DataSet Get_RenewalReport(RenewalSearch objRS)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                con.Open();
                DataSet ds = new DataSet();

                try
                {
                    SqlCommand cmd = new SqlCommand("Usp_GetAllRenwalrecordsFromBetweenDate", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@exactdate", objRS.Exactdate);
                    cmd.Parameters.AddWithValue("@Paymentfromdt", objRS.Fromdate);
                    cmd.Parameters.AddWithValue("@Paymenttodt", objRS.Todate);
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = cmd;
                    da.Fill(ds);

                    if (ds.Tables.Count > 0)
                    {
                        return ds;
                    }

                    else
                    {
                        return ds = null;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    ds.Dispose();
                }

            }
        }
    }
}