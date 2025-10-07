using MediatR;
using MotorcycleFlow.Application.Common.Results;
using MotorcycleFlow.Application.Features.Rentals.DTOs;
using MotorcycleFlow.Application.Interfaces;
using MotorcycleFlow.Core.Entities;
using MotorcycleFlow.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleFlow.Application.Features.Rentals.Commands
{
    public class CreateRentalCommandHandler : IRequestHandler<CreateRentalCommand, Result<RentalDto>>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IDeliveryPersonRepository _deliveryPersonRepository;
        private readonly IRentalCalculatorService _rentalCalculator;

        public CreateRentalCommandHandler(
            IRentalRepository rentalRepository,
            IMotorcycleRepository motorcycleRepository,
            IDeliveryPersonRepository deliveryPersonRepository,
            IRentalCalculatorService rentalCalculator)
        {
            _rentalRepository = rentalRepository;
            _motorcycleRepository = motorcycleRepository;
            _deliveryPersonRepository = deliveryPersonRepository;
            _rentalCalculator = rentalCalculator;
        }

        public async Task<Result<RentalDto>> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // 1. Validate if the delivery person exists and has a type A driver's license
                var deliveryPerson = await _deliveryPersonRepository.GetByIdAsync(request.DeliveryPersonId);
                if (deliveryPerson == null)
                    return Result<RentalDto>.Failure(Error.NotFound);

                if (deliveryPerson.DriverLicenseType != DriverLicenseTypeEnum.A &&
                    deliveryPerson.DriverLicenseType != DriverLicenseTypeEnum.AB)
                    return Result<RentalDto>.Failure(Error.Validation);

                // 2. Validate if motorcycle exists and is available
                var motorcycle = await _motorcycleRepository.GetByIdAsync(request.MotorcycleId);
                if (motorcycle == null || !motorcycle.IsAvailable)
                    return Result<RentalDto>.Failure(Error.Conflict);

                // 3. Calculate total cost
                var totalCost = _rentalCalculator.CalculateTotalCost(
                    (RentalPlanEnum)request.Plan,
                    (request.EndDate - request.StartDate).Days
                );

                // 4. Create lease
                var rental = new Rental(
                    deliveryPersonId: request.DeliveryPersonId,
                    motorcycleId: request.MotorcycleId,
                    startDate: request.StartDate,
                    endDate: request.EndDate,
                    expectedEndDate: request.ExpectedEndDate,
                    plan: (RentalPlanEnum)request.Plan,
                    totalCost: totalCost,
                    status: RentalStatusEnum.Active
                );

                await _rentalRepository.AddAsync(rental);

                // 5. Mark motorcycle as unavailable
                motorcycle.IsAvailable = false;
                await _motorcycleRepository.UpdateAsync(motorcycle);

                // 6. Return DTO
                var rentalDto = new RentalDto
                {
                    identifier = rental.Id,
                    daily_rate = _rentalCalculator.GetDailyRate((RentalPlanEnum)request.Plan),
                    delivery_person_id = rental.DeliveryPersonId,
                    motorcycle_id = rental.MotorcycleId,
                    start_date = rental.StartDate,
                    end_date = rental.EndDate,
                    expected_end_date = rental.ExpectedEndDate
                };

                return Result<RentalDto>.Success(rentalDto);
            }
            catch (Exception ex)
            {
                return Result<RentalDto>.Failure(Error.Failure("Rental.Create", ex.Message));
            }
        }
    }
}
