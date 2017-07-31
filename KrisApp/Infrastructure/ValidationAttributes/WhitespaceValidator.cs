using System.ComponentModel.DataAnnotations;

namespace KrisApp.Infrastructure.ValidationAttributes
{
    public class WhitespaceValidator : ValidationAttribute
    {
        public WhitespaceValidator(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Checks, if value contains whitespaces
        /// </summary>
        public override bool IsValid(object value)
        {
            bool result = false;

            if (value != null && value is string)
            {
                string str = (string)value;
                if (!str.Contains(" "))
                {
                    result = true;
                }
            }

            return result;
        }
    }
}