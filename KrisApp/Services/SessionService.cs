using KrisApp.DataModel.Interfaces;
using System.Web;

namespace KrisApp.Services
{
    public class SessionService : ISessionService
    {
        /// <summary> 
        /// Dodaje element do Sesji [System.Web.HttpContext.Current.Session.Add(name, value)]
        /// </summary>
        public void AddToSession(SessionItem itemName, object value)
        {
            if (HttpContext.Current?.Session != null)
            {
                HttpContext.Current.Session.Add(itemName.ToString(), value);
            }
        }

        /// <summary>
        /// Zwraca element z sesji
        /// </summary>
        public T GetFromSession<T>(SessionItem itemName)
        {
            if (HttpContext.Current?.Session[itemName.ToString()] != null &&
                HttpContext.Current.Session[itemName.ToString()] is T)
            {
                return (T)HttpContext.Current.Session[itemName.ToString()];
            }
            else
            {
                return default(T);
            }
        }

        /// <summary> 
        /// Czyści sesję z zapisanych danych
        /// </summary>
        public void ClearSession()
        {
            if (HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session.Clear();
            }
        }
    }
}