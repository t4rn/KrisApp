using System;
using System.ComponentModel.DataAnnotations;

namespace KrisApp.Models.Pages
{
    public class PageContentModel
    {
        public int ID { get; set; }

        [Display(Name = "Symbol")]
        public string Code { get; set; }

        [Display(Name = "Treść")]
        public string Content { get; set; }

        [Display(Name = "Duch")]
        public bool Ghost { get; set; }

        [Display(Name = "Data dodania")]
        public DateTime AddDate { get; set; }
    }
}