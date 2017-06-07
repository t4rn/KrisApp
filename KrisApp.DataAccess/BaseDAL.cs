using System;
using System.Diagnostics;

namespace KrisApp.DataAccess
{
    public abstract class BaseDAL
    {
        protected readonly string csKris;

        public BaseDAL(string cs)
        {
            csKris = cs;
        }

        protected Action<string> LogDb()
        {
            return msg => Debug.WriteLine(msg);
        }

        /// <summary>
        /// Sprawdza, czy obj jest null'em (lub DBNull.Value) i zwraca nullValue jeśli tak lub zwraca odpowiednio zrzutowaną wartość w przeciwnym wypadku.
        /// </summary>
        protected T GetNotNullableValue<T>(object obj, T nullValue)
        {
            return (obj == null || obj == DBNull.Value) ? nullValue : (T)Convert.ChangeType(obj, typeof(T));// (T)ChangeType(obj, typeof(T));
        }

        /// <summary>
        /// Konwertuje przekazany obiekt na przekazany typ
        /// </summary>
        private object ChangeType(object value, Type type)
        {
            if (type == typeof(int))
            {
                return Convert.ToInt32(value);
            }
            else if (type == typeof(string))
            {
                return Convert.ToString(value);
            }
            else if (type == typeof(decimal))
            {
                return Convert.ToDecimal(value);
            }
            else if (type == typeof(decimal?))
            {
                return Convert.ToDecimal(value);
            }
            else if (type == typeof(DateTime))
            {
                return Convert.ToDateTime(value);
            }
            else if (type == typeof(DateTime?))
            {
                return (DateTime?)value;
            }
            else
            {
                return Convert.ChangeType(value, type);
            }
        }
    }
}