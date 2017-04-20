using System.Linq;

namespace KrisApp.Common.Extensions
{
    public static class StringExtensions
    {
        /// <summary> 
        /// Sprawdza czy String jest równy jednemu z przekazanych stringów 
        /// </summary>
        public static bool In(this string instance, params string[] strings)
        {
            return strings.Contains(instance);
        }
    }
}
