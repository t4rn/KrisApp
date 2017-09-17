using System.Collections.Generic;
using System.Linq;

namespace KrisApp.DataModel.Calc
{
    public class UodSummary
    {
        public decimal Brutto { get; set; }
        public Dictionary<string, decimal> NettoAmounts { get; set; }

        public decimal NettoMax
        {
            get { return NettoAmounts != null ? NettoAmounts.Max(x => x.Value) : 0; }
        }
        public decimal NettoMin
        {
            get { return NettoAmounts != null ? NettoAmounts.Min(x => x.Value) : 0; }
        }

        public decimal Sum
        {
            get { return NettoAmounts != null ? NettoAmounts.Sum(x => x.Value) : 0; }
        }

        public decimal Average
        {
            get { return NettoAmounts != null ? (NettoAmounts.Sum(x => x.Value) / 12) : 0; }
        }
    }
}
