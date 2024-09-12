using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication44.Models
{
    public class Class1
    {
    }
}

    public class DatabaseUtility
    {
        private string connectionString;

        public DatabaseUtility()
        {
            // Get connection string from Web.config
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        // Method for INSERT operation using a stored procedure
        public bool ExecuteStoredProcedure(string procedureName, SqlParameter[] parameters)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(procedureName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    con.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        // Method for SELECT operation using a stored procedure (returns a DataTable)
        public DataTable ExecuteSelectStoredProcedure(string procedureName, SqlParameter[] parameters = null)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(procedureName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }

