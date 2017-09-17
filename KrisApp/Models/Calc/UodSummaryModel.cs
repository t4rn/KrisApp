using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KrisApp.Models.Calc
{
    public class UodSummaryModel
    {
        public string Brutto { get; set; }
        [Display(Name = "Rocznie")]
        public string Sum { get; set; }
        [Display(Name = "Miesięcznie średnio")]
        public string Average { get; set; }
        [Display(Name = "Miesięcznie max")]
        public string NettoMax { get; set; }
        [Display(Name = "Miesięcznie min")]
        public string NettoMin { get; set; }

        public Dictionary<string, decimal> NettoAmounts { get; set; }
    }
}
