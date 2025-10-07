using MediatR;
using MotorcycleFlow.Application.Common.Results;
using MotorcycleFlow.Application.Features.Motorcycles.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleFlow.Application.Features.Motorcycles.Commands
{
    public record UpdateLicensePlateCommand(
       string MotorcycleId,
       string NewLicensePlate
   ) : IRequest<Result<MotorcycleDto>>;
}

