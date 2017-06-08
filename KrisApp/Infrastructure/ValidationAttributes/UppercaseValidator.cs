using System.ComponentModel.DataAnnotations;

namespace KrisApp.Infrastructure.ValidationAttributes
{
    public class UppercaseValidator : ValidationAttribute
    {
        public UppercaseValidator(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Checks, if value starts with an uppercase
        /// </summary>
        public override bool IsValid(object value)
        {
            bool result = false;

            if (value != null && value is string)
            {
                string str = (string)value;
                if (str.Length > 0 && char.IsUpper(str[0]))
                {
                    result = true;
                }
            }

            return result;
        }
    }
}