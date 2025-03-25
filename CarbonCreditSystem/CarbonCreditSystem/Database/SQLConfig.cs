using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace CarbonCreditSystem.Database
{
    public class SQLConfig
    {
        private SqlConnection con = new SqlConnection("Data Source=DESKTOP-I8O0T49\\SQLEXPRESS; Initial Catalog=CarbonCreditDB; TrustServerCertificate=True; Integrated Security=True ");
        private SqlCommand cmd;
        private SqlDataAdapter da;
        public DataTable dt;
        int result;

        public string ExecuteCUD(string sqlQuery)
        {
            string msg = string.Empty;
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = sqlQuery;
                result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    msg = "OK";
                }
                else
                {
                    msg = "ERROR";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }

            return msg;
        }

        public int InsertDataWithReturnId(string sqlQuery)
        {
            int returnID = 0;
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = sqlQuery;
                object x = cmd.ExecuteScalar();
                returnID = Convert.ToInt32(x);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }

            return returnID;
        }


        public DataTable ExecuteSelect(string sqlQuery)
        {
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                cmd = new SqlCommand();
                da = new SqlDataAdapter();
                dt = new DataTable();

                cmd.Connection = con;
                cmd.CommandText = sqlQuery;
                da.SelectCommand = cmd;
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                da.Dispose();
                con.Close();
            }

            return dt;
        }


        public bool ExecuteSelectCheckExistRecords(string sqlQuery)
        {
            try
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                cmd = new SqlCommand();
                da = new SqlDataAdapter();
                dt = new DataTable();

                cmd.Connection = con;
                cmd.CommandText = sqlQuery;
                da.SelectCommand = cmd;
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                da.Dispose();
                con.Close();
            }

        }
    }
}