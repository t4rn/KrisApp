using System.ComponentModel.DataAnnotations;

namespace KrisApp.Models.User
{
    public class UserRegisterModel
    {
        [Required(ErrorMessage = "Nazwa użytkownika jest wymagana.")]
        [Display(Name = "Użytkownik")]
        [StringLength(12, MinimumLength = 3, ErrorMessage = "Login musi zawierać od 3 do 12 znaków.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane.")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        [StringLength(12, MinimumLength = 3, ErrorMessage = "Hasło musi zawierać od 3 do 12 znaków.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Hasła nie są identyczne.")]
        [Required(ErrorMessage = "Nowe hasło jest wymagane.")]
        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [StringLength(12, MinimumLength = 3, ErrorMessage = "Hasło musi zawierać od 3 do 12 znaków.")]
        public string PasswordRepeated { get; set; }

        [Required(ErrorMessage = "Adres e-mail jest wymagany.")]
        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "Niepoprawny adres e-mail")]
        public string Email { get; set; }

        [Display(Name = "Komentarz")]
        [StringLength(256, ErrorMessage = "Maksymalna liczba znaków wynosi 256")]
        public string Comment { get; set; }
    }
}