using System.ComponentModel.DataAnnotations;

namespace KrisApp.Models.Me
{
    public class ContactModel
    {
        [Display(Name = "Nadawca")]
        [Required]
        [StringLength(64, ErrorMessage = "Pole może zawierać od 2 do 64 znaków", MinimumLength = 2)]
        public string Author { get; set; }

        [Display(Name = "Temat")]
        [StringLength(64, ErrorMessage = "Pole może zawierać od 4 do 64 znaków", MinimumLength = 4)]
        public string Subject { get; set; }

        [Display(Name = "Wiadomość")]
        [StringLength(256, ErrorMessage = "Pole może zawierać maksymalnie 256 znaków")]
        public string Content { get; set; }
    }
}