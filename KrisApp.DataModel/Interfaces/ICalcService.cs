using KrisApp.DataModel.Calc;

namespace KrisApp.DataModel.Interfaces
{
    public interface ICalcService
    {
        /// <summary>
        /// Calculates income pre month based on cost limit
        /// </summary>
        UodSummary CalculateIncome(decimal bruttoPerMonth, decimal limit);
    }
}
