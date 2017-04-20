using KrisApp.DataModel.Dictionaries;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrisApp.DataModel.Article
{
    [Table("Article", Schema = "Work")]
    public class Article
    {
        public int Id { get; set; }

        [ForeignKey("Type")]
        public int TypeId { get; set; }

        public ArticleType Type { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        [Column("Ghost")]
        public bool IsGhost { get; set; }

        public DateTime AddDate { get; set; }
    }
}
