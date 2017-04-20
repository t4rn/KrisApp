using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KrisApp.Models.Articles
{
    public class ArticleHomeModel
    {
        public ArticleTypeModel MainArticle { get; set; }
        public List<List<ArticleTypeModel>> SideArticles { get; set; }
        //public List<ArticleTypeModel> SideArticles { get; set; }
    }
}