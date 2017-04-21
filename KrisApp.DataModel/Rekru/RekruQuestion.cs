using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrisApp.DataModel.Rekru
{
    [Table("RekruQuestions", Schema = "WWW")]
    public class RekruQuestion
    {
        [Key]
        public int ID { get; set; }

        public string Question { get; set; }

        public string Author { get; set; }

        public DateTime AddDate { get; set; }

        public bool Ghost { get; set; }
    }
}