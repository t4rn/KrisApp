using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace KrisApp.Models.Calc
{
    public class UodModel
    {
        [Display(Name = "Limit kosztów")]
        public decimal Limit { get; set; }

        [Display(Name = "Kwota brutto")]
        public decimal BruttoAmountPerMonth { get; set; }

        public Dictionary<string, decimal> NettoAmounts { get; set; }

        public string Sum
        {
            get
            {
                return NettoAmounts != null ? NettoAmounts.Sum(x => x.Value).ToString() : "0";
            }
        }

        public string AverageIncomeNetto
        {
            get
            {
                return NettoAmounts != null ? (NettoAmounts.Sum(x => x.Value) / 12).ToString() : "";
            }
        }

    }
}
