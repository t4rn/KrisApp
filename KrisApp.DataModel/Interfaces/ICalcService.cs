using System.Collections.Generic;

namespace KrisApp.DataModel.Interfaces
{
    public interface ICalcService
    {
        /// <summary>
        /// Calculates income pre month based on cost limit
        /// </summary>
        Dictionary<string, decimal> CalculateIncome(decimal bruttoAmountPerMonth, decimal limit);
    }
}
