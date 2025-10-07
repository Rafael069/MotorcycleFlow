using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MotorcycleFlow.Application.Common.Results;
using MotorcycleFlow.Application.Features.Motorcycles.DTOs;

namespace MotorcycleFlow.Application.Features.Motorcycles.Commands
{
    public record CreateMotorcycleCommand(
         string Identifier,
         int Year,
         string Model,
         string LicensePlate
     ) : IRequest<Result<MotorcycleDto>>;
}
