using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KrisApp.Models.Questions
{
    public class QuestionModel
    {
        public int ID { get; set; }

        [Display(Name = "Treść pytania")]
        public string Question { get; set; }

        [Display(Name = "Autor")]
        public string Author { get; set; }

        [Display(Name = "Data dodania")]
        public DateTime AddDate { get; set; }

        [Display(Name = "Odpowiedzi")]
        public List<AnswerModel> Answers { get; set; }
    }
}