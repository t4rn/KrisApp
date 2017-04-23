﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace KrisApp.Models.Articles
{
    public class ArticleModel
    {
        public int ID { get; set; }

        [Display(Name = "Typ artykułu")]
        public int TypeId { get; set; }

        [Required]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [MinLength(20)]
        [Display(Name = "Treść artykułu")]
        public string Content { get; set; }

        [Display(Name = "Autor")]
        public string Author { get; set; }

        [Display(Name = "Czy zduchowany")]
        public bool IsGhost { get; set; }

        [Display(Name = "Data dodania")]
        public DateTime AddDate { get; set; }

        public IEnumerable<SelectListItem> ArticleTypes { get; set; }
    }
}