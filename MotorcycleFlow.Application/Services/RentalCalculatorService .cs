using MotorcycleFlow.Application.Interfaces;
using MotorcycleFlow.Core.Entities;
using MotorcycleFlow.Core.Enums;

namespace MotorcycleFlow.Application.Services
{
    public class RentalCalculatorService : IRentalCalculatorService
    {
        private readonly Dictionary<RentalPlanEnum, decimal> _dailyRates = new()
        {
            { RentalPlanEnum.SevenDays, 30.00m },
            { RentalPlanEnum.FifteenDays, 28.00m },
            { RentalPlanEnum.ThirtyDays, 22.00m },
            { RentalPlanEnum.FortyFiveDays, 20.00m },
            { RentalPlanEnum.FiftyDays, 18.00m }
        };

        private readonly Dictionary<RentalPlanEnum, decimal> _earlyReturnPenalties = new()
        {
            { RentalPlanEnum.SevenDays, 0.20m },    // 20%
            { RentalPlanEnum.FifteenDays, 0.40m },  // 40%
            { RentalPlanEnum.ThirtyDays, 0.00m },
            { RentalPlanEnum.FortyFiveDays, 0.00m },
            { RentalPlanEnum.FiftyDays, 0.00m }
        };

        private const decimal LATE_RETURN_PENALTY = 50.00m; // R$50 por dia

        public decimal CalculateTotalCost(RentalPlanEnum plan, int days)
        {
            if (!_dailyRates.ContainsKey(plan))
                throw new ArgumentException("Invalid rental plan");

            return _dailyRates[plan] * days;
        }

        public decimal CalculateFinalCost(Rental rental, DateTime actualReturnDate)
        {
            var baseCost = rental.TotalCost;

            if (actualReturnDate < rental.ExpectedEndDate)
            {
                // Devolução antecipada - aplicar multa
                var penalty = CalculateEarlyReturnPenalty(rental, actualReturnDate);
                return baseCost - penalty;
            }
            else if (actualReturnDate > rental.ExpectedEndDate)
            {
                // Devolução tardia - aplicar multa por dia adicional
                var penalty = CalculateLateReturnPenalty(rental, actualReturnDate);
                return baseCost + penalty;
            }

            // Devolução no prazo
            return baseCost;
        }

        public decimal GetDailyRate(RentalPlanEnum plan)
        {
            return _dailyRates.ContainsKey(plan) ? _dailyRates[plan] : 0;
        }

        public decimal CalculateEarlyReturnPenalty(Rental rental, DateTime actualReturnDate)
        {
            if (!_earlyReturnPenalties.ContainsKey(rental.Plan) || _earlyReturnPenalties[rental.Plan] == 0)
                return 0;

            var daysNotUsed = (rental.ExpectedEndDate - actualReturnDate).Days;
            var dailyRate = GetDailyRate(rental.Plan);
            var unusedCost = dailyRate * daysNotUsed;

            return unusedCost * _earlyReturnPenalties[rental.Plan];
        }

        public decimal CalculateLateReturnPenalty(Rental rental, DateTime actualReturnDate)
        {
            var extraDays = (actualReturnDate - rental.ExpectedEndDate).Days;
            return extraDays * LATE_RETURN_PENALTY;
        }
    }
}
