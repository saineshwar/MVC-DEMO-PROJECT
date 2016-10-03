using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace GYMONE.Repository
{
    public class Receipt : IReceipt 
    {
        public DataSet GenerateRecepitDataset(string MemberID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                con.Open();
                DataSet ds = new DataSet();

                try
                {
                    SqlCommand cmd = new SqlCommand("Usp_GenerateRecepit", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Memberid", MemberID);
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

        public DataSet GenerateDeclarationDataset(string MemberID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                con.Open();
                DataSet ds = new DataSet();

                try
                {
                    SqlCommand cmd = new SqlCommand("Usp_getDec", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Memberid", MemberID);
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