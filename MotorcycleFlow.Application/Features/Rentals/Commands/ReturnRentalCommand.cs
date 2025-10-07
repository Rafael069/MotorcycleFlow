using MediatR;
using MotorcycleFlow.Application.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleFlow.Application.Features.Rentals.Commands
{
    public record ReturnRentalCommand(
    string RentalId,
    DateTime ActualReturnDate
) : IRequest<Result<decimal>>; // Returns final cost
}

