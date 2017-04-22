using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrisApp.DataModel.Rekru
{
    [Table("RekruAnswers", Schema = "WWW")]
    public class RekruAnswer
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Question")]
        public int QuestionID { get; set; }
        public RekruQuestion Question { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public DateTime AddDate { get; set; }

        public bool Ghost { get; set; }
    }
}