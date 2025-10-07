using MediatR;
using MotorcycleFlow.Application.Common.Results;
using MotorcycleFlow.Application.Features.Rentals.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleFlow.Application.Features.Rentals.Commands
{
    public record CreateRentalCommand(
    string DeliveryPersonId,
    string MotorcycleId,
    DateTime StartDate,
    DateTime EndDate,
    DateTime ExpectedEndDate,
    int Plan
) : IRequest<Result<RentalDto>>;
}
