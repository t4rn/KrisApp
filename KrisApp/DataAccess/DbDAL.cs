using KrisApp.DataModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KrisApp.DataAccess
{
    public class DbDAL
    {
        public static List<Masterplan> GetMasterplans()
        {
            List<Masterplan> masts = new List<Masterplan>();
            string query = @"SELECT id, description, add_date FROM kw.masterplan";

            using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.csDB))
            {
                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    string ne = dr["description"].ToString();
                    masts.Add(new Masterplan() { Description = ne });
                }
            }

            return masts;
        }
    }
}