using System.ComponentModel.DataAnnotations;

namespace KrisApp.Models.Rekru
{
    public class QuestionAddModel
    {
        [Display(Name = "Treść pytania")]
        public string Question { get; set; }
        [Display(Name = "Autor")]
        public string Author { get; set; }
    }
}