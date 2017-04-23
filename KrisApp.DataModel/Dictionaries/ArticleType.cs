using System.ComponentModel.DataAnnotations.Schema;

namespace KrisApp.DataModel.Dictionaries
{
    [Table("ArticleType", Schema = "Work")]
    public class ArticleType : DictionaryItem
    {
        [Column("Descr")]
        public string Description { get; set; }

        public bool IsMain { get; set; }

        public ArticleTypeCode[] CodeTypes
        {
            get
            {
                return new ArticleTypeCode[] {
                ArticleTypeCode.ASP, ArticleTypeCode.PATTERN, ArticleTypeCode.SQL, ArticleTypeCode.WCF };
            }
        }
        public enum ArticleTypeCode
        {
            ASP,
            WCF,
            PATTERN,
            SQL,
            REKRU
        }
    }
}
