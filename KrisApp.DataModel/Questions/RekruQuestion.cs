using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KrisApp.DataModel.Questions
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

        public List<RekruAnswer> Answers { get; set; }
    }
}