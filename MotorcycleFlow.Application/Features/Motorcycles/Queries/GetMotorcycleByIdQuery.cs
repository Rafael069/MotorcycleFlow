using MediatR;
using MotorcycleFlow.Application.Common.Results;
using MotorcycleFlow.Application.Features.Motorcycles.DTOs;

namespace MotorcycleFlow.Application.Features.Motorcycles.Queries
{
    public record GetMotorcycleByIdQuery(string MotorcycleId)
        : IRequest<Result<MotorcycleDto>>;
}
