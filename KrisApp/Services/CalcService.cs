using KrisApp.Common.Extensions;
using KrisApp.DataModel.Calc;
using KrisApp.DataModel.Interfaces;
using System;
using System.Collections.Generic;

namespace KrisApp.Services
{
    public class CalcService : AbstractService, ICalcService
    {
        public CalcService(ILogger log) : base(log)
        {
        }

        public UodSummary CalculateIncome(decimal bruttoPerMonth, decimal limit)
        {
            UodSummary summary = new UodSummary();
            _log.Debug($"[{nameof(CalculateIncome)}] Calculating for brutto = '{bruttoPerMonth}' and limit = '{limit}'");

            decimal sumBrutto = 0;
            decimal nettoPerMonth = 0;
            decimal multiplier = 0;
            bool isLower = false;
            Dictionary<string, decimal> amounts = new Dictionary<string, decimal>();

            for (int i = 1; i <= 12; i++)
            {
                sumBrutto = sumBrutto + bruttoPerMonth;
                if (sumBrutto > limit * 2 && isLower == false)
                {
                    decimal newPartialBrutto = sumBrutto - limit * 2; // 90000 - 85528 = 4472
                    decimal newPartialNetto = newPartialBrutto * 0.82M;

                    decimal oldPartialBrutto = bruttoPerMonth - newPartialBrutto;
                    decimal oldPartialNetto = oldPartialBrutto * 0.91M;

                    nettoPerMonth = Math.Round(newPartialNetto + oldPartialNetto);
                    isLower = true;
                }
                else
                {
                    multiplier = isLower ? 0.82M : 0.91M;
                    nettoPerMonth = bruttoPerMonth * multiplier;
                }

                amounts.Add(i.GetMonthName(), nettoPerMonth);
            }

            summary.NettoAmounts = amounts;
            summary.Brutto = bruttoPerMonth;

            return summary;
        }
    }
}
