using MediatR;
using MotorcycleFlow.Application.Common.Results;
using MotorcycleFlow.Application.Features.DeleveryPerson.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleFlow.Application.Features.DeleveryPerson.Commands
{
    public record CreateDeliveryPersonCommand(
    string Identifier,
    string Name,
    string CNPJ,
    DateTime BirthDate,
    string DriverLicenseNumber,
    string DriverLicenseType, // "A", "B", "A+B"
    string DriverLicenseImageBase64
) : IRequest<Result<DeliveryPersonDto>>;
}
