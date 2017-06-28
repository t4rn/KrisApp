using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrisApp.DataModel.Pages
{
    [Table("PageContent", Schema = "WWW")]
    public class PageContent
    {
        [Key]
        public int ID { get; set; }

        public string Code { get; set; }

        public string Content { get; set; }

        public bool Ghost { get; set; }

        public DateTime AddDate { get; set; }

        public enum Type
        {
            About, Website
        }
    }
}
