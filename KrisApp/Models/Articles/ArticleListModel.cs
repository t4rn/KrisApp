using System.Collections.Generic;

namespace KrisApp.Models.Articles
{
    public class ArticleListModel
    {
        public List<ArticleDetailsModel> Articles { get; set; }

        public bool? IsMod { get; set; }
    }
}