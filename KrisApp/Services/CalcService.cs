using KrisApp.Common.Extensions;
using KrisApp.DataModel.Interfaces;
using System.Collections.Generic;

namespace KrisApp.Services
{
    public class CalcService : AbstractService, ICalcService
    {
        public CalcService(ILogger log) : base(log)
        {
        }

        public Dictionary<string, decimal> CalculateIncome(decimal bruttoAmountPerMonth, decimal limit)
        {
            _log.Debug($"[{nameof(CalculateIncome)}] Calculating for brutto = '{bruttoAmountPerMonth}' and limit = '{limit}'");

            decimal sum = 0;
            decimal monthlyAmount = 0;
            decimal multiplier = 0;

            Dictionary<string, decimal> amounts = new Dictionary<string, decimal>();

            for (int i = 1; i <= 12; i++)
            {
                sum = sum + bruttoAmountPerMonth;

                multiplier = sum > limit * 2 ? 0.82M : 0.91M;

                monthlyAmount = bruttoAmountPerMonth * multiplier;

                amounts.Add(i.GetMonthName(), monthlyAmount);
            }

            return amounts;
        }
    }
}
