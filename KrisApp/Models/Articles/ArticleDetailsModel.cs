using System;
using System.ComponentModel.DataAnnotations;

namespace KrisApp.Models.Articles
{
    public class ArticleDetailsModel
    {
        public int ID { get; set; }

        [Display(Name = "Typ")]
        public string TypeName { get; set; }

        [Display(Name = "Symbol artykułu")]
        public string TypeCode { get; set; }

        [Required]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [MinLength(20)]
        [Display(Name = "Treść artykułu")]
        public string Content { get; set; }

        [Display(Name = "Autor")]
        public string Author { get; set; }

        [Display(Name = "Czy zduchowany")]
        public bool IsGhost { get; set; }

        [Display(Name = "Data dodania")]
        public DateTime AddDate { get; set; }
    }
}