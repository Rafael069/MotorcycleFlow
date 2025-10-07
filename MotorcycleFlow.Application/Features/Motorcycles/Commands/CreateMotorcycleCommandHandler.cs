using MediatR;
using MotorcycleFlow.Application.Common.Results;
using MotorcycleFlow.Application.Features.Motorcycles.DTOs;
using MotorcycleFlow.Application.Interfaces;
using MotorcycleFlow.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Error = MotorcycleFlow.Application.Common.Results.Error;

namespace MotorcycleFlow.Application.Features.Motorcycles.Commands
{
    public class CreateMotorcycleCommandHandler : IRequestHandler<CreateMotorcycleCommand, Result<MotorcycleDto>>
    {
        private readonly IMotorcycleRepository _motorcycleRepository;

        public CreateMotorcycleCommandHandler(IMotorcycleRepository motorcycleRepository)
        {
            _motorcycleRepository = motorcycleRepository;
        }

        public async Task<Result<MotorcycleDto>> Handle(CreateMotorcycleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // 1. Validar se placa já existe
                var existingMotorcycle = await _motorcycleRepository.GetByLicensePlateAsync(request.LicensePlate);
                if (existingMotorcycle != null)
                {
                    return Result<MotorcycleDto>.Failure(Error.Conflict);
                }

                // 2. Criar a moto
                var motorcycle = new Motorcycle(
                    identifier: request.Identifier,
                    year: request.Year,
                    model: request.Model,
                    licensePlate: request.LicensePlate.ToUpper()
                );

                // 3. Salvar no banco
                await _motorcycleRepository.AddAsync(motorcycle);

                // 4. Retornar DTO
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
                return Result<MotorcycleDto>.Failure(Error.Failure("Motorcycle.Create", ex.Message));
            }
        }
    }

}
