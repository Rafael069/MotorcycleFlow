using MediatR;
using MotorcycleFlow.Application.Common.Results;
using MotorcycleFlow.Application.Features.Motorcycles.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleFlow.Application.Features.Motorcycles.Queries
{
    public record GetMotorcyclesQuery(string? LicensePlate = null)
    : IRequest<Result<List<MotorcycleDto>>>;
}
