using MediatR;
using MotorcycleFlow.Application.Common.Results;
using MotorcycleFlow.Application.Features.Motorcycles.DTOs;
using MotorcycleFlow.Application.Interfaces;

namespace MotorcycleFlow.Application.Features.Motorcycles.Queries
{
    public class GetMotorcyclesQueryHandler : IRequestHandler<GetMotorcyclesQuery, Result<List<MotorcycleDto>>>
    {
        private readonly IMotorcycleRepository _motorcycleRepository;

        public GetMotorcyclesQueryHandler(IMotorcycleRepository motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository;
        }

        public async Task<Result<List<MotorcycleDto>>> Handle(GetMotorcyclesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // ✅ Usando seu método GetAllAsync com filtro
                var motorcycles = await _motorcycleRepository.GetAllAsync(request.LicensePlate);

                var dtos = motorcycles.Select(m => new MotorcycleDto
                {
                    Id = m.Id,
                    Identifier = m.Identifier,
                    Year = m.Year,
                    Model = m.Model,
                    LicensePlate = m.LicensePlate,
                    IsAvailable = m.IsAvailable
                }).ToList();

                return Result<List<MotorcycleDto>>.Success(dtos);
            }
            catch (Exception ex)
            {
                return Result<List<MotorcycleDto>>.Failure(Error.Failure("Motorcycle.GetAll", ex.Message)); 
            }
        }
    }
}