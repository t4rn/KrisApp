using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace KrisApp.Models.Articles
{
    public class ArticleCreateModel
    {
        //public ArticleModel ArticleToCreate { get; set; }

        public int TypeId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public IEnumerable<SelectListItem> ArticleTypes { get; set; }
    }
}