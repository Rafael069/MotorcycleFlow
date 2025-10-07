using MediatR;
using MotorcycleFlow.Application.Common.Results;
using MotorcycleFlow.Application.Features.Motorcycles.DTOs;
using MotorcycleFlow.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleFlow.Application.Features.Motorcycles.Commands
{
    public class UpdateLicensePlateCommandHandler : IRequestHandler<UpdateLicensePlateCommand, Result<MotorcycleDto>>
    {
        private readonly IMotorcycleRepository _motorcycleRepository;

        public UpdateLicensePlateCommandHandler(IMotorcycleRepository motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository;
        }

        public async Task<Result<MotorcycleDto>> Handle(UpdateLicensePlateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // 1. Buscar a moto
                var motorcycle = await _motorcycleRepository.GetByIdAsync(request.MotorcycleId);
                if (motorcycle == null)
                {
                    return Result<MotorcycleDto>.Failure(Error.NotFound);
                }

                // 2. Validar se a nova placa já existe em outra moto
                var existingWithPlate = await _motorcycleRepository.GetByLicensePlateAsync(request.NewLicensePlate);
                if (existingWithPlate != null && existingWithPlate.Id != request.MotorcycleId)
                {
                    return Result<MotorcycleDto>.Failure(Error.Conflict);
                }

                // 3. Atualizar a placa
                motorcycle.LicensePlate = request.NewLicensePlate.ToUpper();
                await _motorcycleRepository.UpdateAsync(motorcycle);

                // 4. Retornar DTO atualizado
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
                return Result<MotorcycleDto>.Failure(Error.Failure("Motorcycle.UpdateLicensePlate", ex.Message));
            }
        }
    }
}

