using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KrisApp.Models.Calc
{
    public class UodModel
    {
        public UodModel()
        {
            SavedSummaries = new List<UodSummaryModel>();
        }

        [Display(Name = "Limit kosztów")]
        public decimal Limit { get; set; }

        [Display(Name = "Kwota brutto")]
        [Range(1, 1000000)]
        public decimal BruttoAmountPerMonth { get; set; }

        public UodSummaryModel CurrentSummary { get; set; }

        public List<UodSummaryModel> SavedSummaries { get; set; }
    }
}
