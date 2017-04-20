using System.ComponentModel.DataAnnotations;

namespace KrisApp.Models.User
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Nazwa użytkownika jest wymagana.")]
        [Display(Name = "Użytkownik")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane.")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        [StringLength(12, MinimumLength = 3, ErrorMessage = "Hasło powinno zawierać od 3 do 12 znaków.")]
        public string Password { get; set; }
    }
}