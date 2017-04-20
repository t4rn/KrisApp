using KrisApp.DataAccess.DbContexts;
using KrisApp.DataModel.Interfaces.Repositories;
using KrisApp.DataModel.Logs;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace KrisApp.DataAccess
{
    public class AppLogRepo : BaseDAL, ILogRepository
    {
        public AppLogRepo(string cs) : base(cs)
        { }

        /// <summary>
        /// Zapisuje przekazaną wiadomość na bazie [WWW.Logs]
        /// </summary>
        public void AddLog(AppLog log)
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

        /// <summary>
        /// Zwraca wszystkie logi posortowane malejąco
        /// </summary>
        public List<AppLog> GetLogs()
        {
            List<AppLog> appLogs = null;

            using (KrisDbContext context = new KrisDbContext(csKris))
            {
                appLogs = context.AppLogs.AsNoTracking()
                    .OrderByDescending(x => x.ID).ToList();
            }

            return appLogs;
        }
    }
}