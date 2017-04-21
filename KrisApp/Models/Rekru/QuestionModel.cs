using System;

namespace KrisApp.Models.Rekru
{
    public class QuestionModel
    {
        public int ID { get; set; }

        public string Question { get; set; }

        public string Author { get; set; }

        public DateTime AddDate { get; set; }
    }
}