using System.Web;

namespace KrisApp.Services
{
    public static class NetworkService
    {
        /// <summary> Zwraca IP użytkownika korzystającego z aplikacji
        /// </summary>
        internal static string GetContextIP()
        {
            string ip = HttpContext.Current.Request.UserHostAddress;

            //// jeżeli IP żądania to IP load balancera to wyciągamy IP z X-Forwarded-For
            //if (ip == Properties.Settings.Default.HttpsProxyIP &&
            //    HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null &&
            //    HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != "")
            //{
            //    ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            //    if (ip.Contains(","))
            //    {
            //        ip = ip.Split(',').First().Trim();
            //    }

            //}

            return ip;
        }
    }
}