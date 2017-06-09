using System;
using System.ComponentModel.DataAnnotations;

namespace KrisApp.Models.Questions
{
    public class AnswerModel
    {
        public int ID { get; set; }

        [Display(Name = "Treść odpowiedzi")]
        public string Content { get; set; }

        [Display(Name = "Autor")]
        public string Author { get; set; }

        [Display(Name = "Data dodania")]
        public DateTime AddDate { get; set; }

        public int QuestionID { get; set; }
    }
}