using System;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace KrisApp.Infrastructure.AuthenticationFilters
{
    public class HttpAuthenticate : FilterAttribute, IAuthenticationFilter
    {
        private readonly string _password;
        private readonly string _username;

        public HttpAuthenticate(string username, string password)
        {
            _username = username;
            _password = password;
        }
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            string authHeader = filterContext.HttpContext.Request.Headers["Authorization"];
            if (!string.IsNullOrEmpty(authHeader))
            {
                string[] credentials = ASCIIEncoding.ASCII.GetString(
                    Convert.FromBase64String(authHeader.Replace("Basic", ""))).Split(':');

                string username = credentials[0];
                string password = credentials[1];

                if (username != _username || password != _password)
                {
                    filterContext.Result = new HttpUnauthorizedResult("Bad username or password.");
                }
            }
            else
            {
                filterContext.Result = new HttpUnauthorizedResult("Authorization missing in HTTP Header.");
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
        }
    }
}