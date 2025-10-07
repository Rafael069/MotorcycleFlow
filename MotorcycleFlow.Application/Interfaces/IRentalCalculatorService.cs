using MotorcycleFlow.Core.Entities;
using MotorcycleFlow.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleFlow.Application.Interfaces
{
    public interface IRentalCalculatorService
    {
        decimal CalculateTotalCost(RentalPlanEnum plan, int days);
        decimal CalculateFinalCost(Rental rental, DateTime actualReturnDate);
        decimal GetDailyRate(RentalPlanEnum plan);
        decimal CalculateEarlyReturnPenalty(Rental rental, DateTime actualReturnDate);
        decimal CalculateLateReturnPenalty(Rental rental, DateTime actualReturnDate);
    }
}
