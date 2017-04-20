using System.ComponentModel.DataAnnotations.Schema;

namespace KrisApp.DataModel.Dictionaries
{
    [Table("AppUserType", Schema = "Work")]
    public class UserType : DictionaryItem
    {
        /// <summary>
        /// Symbole typów użytkowników
        /// </summary>
        public enum UserTypeCodes
        {
            USR, ADM, MOD
        }
    }
}