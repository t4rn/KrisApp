using KrisApp.DataModel.Logs;
using System.Data.SqlClient;

namespace KrisApp.DataAccess
{
    public class AppLogDAL : BaseDAL
    {
        public AppLogDAL(string cs) : base(cs)
        {}

        /// <summary>
        /// Zapisuje przekazaną wiadomość na bazie [WWW.Logs]
        /// </summary>
        internal void AddLog(AppLog log)
        {
            string query = @"INSERT INTO WWW.Logs (Type, Message, Ip) VALUES (@type, @msg, @ip);";

            using (SqlConnection conn = new SqlConnection(csKris))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@type", log.Type.ToString());
                cmd.Parameters.AddWithValue("@msg", log.Message);
                cmd.Parameters.AddWithValue("@ip", log.Ip);

                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}