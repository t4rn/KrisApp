using System.ComponentModel.DataAnnotations.Schema;

namespace KrisApp.DataModel.Dictionaries
{
    [Table("ArticleType", Schema = "Work")]
    public class ArticleType : DictionaryItem
    {
        [Column("Descr")]
        public string Description { get; set; }

        public bool IsMain { get; set; }
    }
}
