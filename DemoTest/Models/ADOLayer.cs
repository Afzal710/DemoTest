using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace DemoTest.Models
{
    public class ADOLayer
    {
        SqlConnection conn;
        public ADOLayer()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["demo"].ConnectionString);
        }
        public int ExecuteDML(string procedure, SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand(procedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter param in parameters)
            {
                cmd.Parameters.Add(param);
            }
            conn.Open();
            int r = cmd.ExecuteNonQuery();
            conn.Close();
            return r;
        }
        public DataTable DataDisplay(string procedure, SqlParameter[] parameters)
        {
            SqlCommand cmd = new SqlCommand(procedure, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter para in parameters)
            {
                if (para.Value != null)
                {
                    cmd.Parameters.Add(para);
                }
            }
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dt);
            return dt;
        }
    }
}