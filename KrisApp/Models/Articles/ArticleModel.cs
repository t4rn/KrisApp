using System.ComponentModel.DataAnnotations;

namespace KrisApp.Models.Articles
{
    public class ArticleModel
    {
        public int TypeId { get; set; }

        [Required]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }
    }
}