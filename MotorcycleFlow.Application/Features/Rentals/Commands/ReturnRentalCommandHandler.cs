using MediatR;
using MotorcycleFlow.Application.Common.Results;
using MotorcycleFlow.Application.Interfaces;
using MotorcycleFlow.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleFlow.Application.Features.Rentals.Commands
{
    public class ReturnRentalCommandHandler : IRequestHandler<ReturnRentalCommand, Result<decimal>>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IRentalCalculatorService _rentalCalculator;

        public ReturnRentalCommandHandler(
            IRentalRepository rentalRepository,
            IMotorcycleRepository motorcycleRepository,
            IRentalCalculatorService rentalCalculator)
        {
            _rentalRepository = rentalRepository;
            _motorcycleRepository = motorcycleRepository;
            _rentalCalculator = rentalCalculator;
        }

        public async Task<Result<decimal>> Handle(ReturnRentalCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // 1. Buscar locação
                var rental = await _rentalRepository.GetByIdAsync(request.RentalId);
                if (rental == null)
                    return Result<decimal>.Failure(Error.NotFound);

                // 2. Calcular custo final com multas
                var finalCost = _rentalCalculator.CalculateFinalCost(
                    rental,
                    request.ActualReturnDate
                );

                // 3. Atualizar locação
                rental.EndDate = request.ActualReturnDate;
                rental.TotalCost = finalCost;
                rental.Status = RentalStatusEnum.Completed;
                await _rentalRepository.UpdateAsync(rental);

                // 4. Liberar moto
                var motorcycle = await _motorcycleRepository.GetByIdAsync(rental.MotorcycleId);
                if (motorcycle != null)
                {
                    motorcycle.IsAvailable = true;
                    await _motorcycleRepository.UpdateAsync(motorcycle);
                }

                return Result<decimal>.Success(finalCost);
            }
            catch (Exception ex)
            {
                return Result<decimal>.Failure(Error.Failure("Rental.Return", ex.Message));
            }
        }
    }
}
