using System.Linq;

namespace KrisApp.Common.Extensions
{
    public static class IntExtensions
    {
        /// <summary> 
        /// Checks, if Int is equal to one of the provided ints
        /// </summary>
        public static bool In(this int instance, params int[] ints)
        {
            return ints.Contains(instance);
        }

        /// <summary>
        /// Returns month name by it's number
        /// </summary>
        public static string GetMonthName(this int instance)
        {
            string month;

            switch (instance)
            {
                case 1:
                    month = "styczeń";
                    break;
                case 2:
                    month = "luty";
                    break;
                case 3:

                    month = "marzec";
                    break;
                case 4:
                    month = "kwiecień";
                    break;
                case 5:
                    month = "maj";
                    break;
                case 6:
                    month = "czerwiec";
                    break;
                case 7:
                    month = "lipiec";
                    break;
                case 8:
                    month = "sierpień";
                    break;
                case 9:
                    month = "wrzesień";
                    break;
                case 10:
                    month = "październik";
                    break;
                case 11:
                    month = "listopad";
                    break;
                case 12:
                    month = "grudzień";
                    break;
                default:
                    month = "błąd...";
                    break;
            }

            return month;
        }
    }
}
