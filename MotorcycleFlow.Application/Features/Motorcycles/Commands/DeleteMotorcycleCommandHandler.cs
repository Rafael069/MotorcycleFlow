using MediatR;
using MotorcycleFlow.Application.Common.Results;
using MotorcycleFlow.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleFlow.Application.Features.Motorcycles.Commands
{
    public class DeleteMotorcycleCommandHandler : IRequestHandler<DeleteMotorcycleCommand, Result<bool>>
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IRentalRepository _rentalRepository;

        public DeleteMotorcycleCommandHandler(
            IMotorcycleRepository motorcycleRepository,
            IRentalRepository rentalRepository)
        {
            _motorcycleRepository = motorcycleRepository;
            _rentalRepository = rentalRepository;
        }

        public async Task<Result<bool>> Handle(DeleteMotorcycleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // 1. Buscar a moto
                var motorcycle = await _motorcycleRepository.GetByIdAsync(request.MotorcycleId);
                if (motorcycle == null)
                {
                    return Result<bool>.Failure(Error.NotFound);
                }

                // 2. Verificar se tem locações ativas (REQUISITO)
                var hasActiveRentals = await _rentalRepository.HasActiveRentalsAsync(request.MotorcycleId);
                if (hasActiveRentals)
                {
                    return Result<bool>.Failure(Error.Conflict);
                }

                // 3. Deletar a moto
                await _motorcycleRepository.DeleteAsync(motorcycle);

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(Error.Failure("Motorcycle.Delete", ex.Message));
            }
        }
    }
}

