using System.Linq;

namespace KrisApp.Common.Extensions
{
    public static class IntExtensions
    {
        /// <summary> 
        /// Sprawdza czy Int jest równy jednemu z przekazanych intów
        /// </summary>
        public static bool In(this int instance, params int[] ints)
        {
            return ints.Contains(instance);
        }
    }
}
