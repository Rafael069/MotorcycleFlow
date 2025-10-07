using MediatR;
using MotorcycleFlow.Application.Common.Results;
using MotorcycleFlow.Application.Features.Motorcycles.DTOs;
using MotorcycleFlow.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleFlow.Application.Features.Motorcycles.Queries
{
    public class GetMotorcycleByIdQueryHandler : IRequestHandler<GetMotorcycleByIdQuery, Result<MotorcycleDto>>
    {
        private readonly IMotorcycleRepository _motorcycleRepository;

        public GetMotorcycleByIdQueryHandler(IMotorcycleRepository motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository;
        }

        public async Task<Result<MotorcycleDto>> Handle(GetMotorcycleByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var motorcycle = await _motorcycleRepository.GetByIdAsync(request.MotorcycleId);

                if (motorcycle == null)
                {
                    return Result<MotorcycleDto>.Failure(Error.NotFound);
                }

                var motorcycleDto = new MotorcycleDto
                {
                    Id = motorcycle.Id,
                    Identifier = motorcycle.Identifier,
                    Year = motorcycle.Year,
                    Model = motorcycle.Model,
                    LicensePlate = motorcycle.LicensePlate,
                    IsAvailable = motorcycle.IsAvailable
                };

                return Result<MotorcycleDto>.Success(motorcycleDto);
            }
            catch (Exception ex)
            {
                return Result<MotorcycleDto>.Failure(Error.Failure("Motorcycle.GetById", ex.Message));
            }
        }
    }
}
