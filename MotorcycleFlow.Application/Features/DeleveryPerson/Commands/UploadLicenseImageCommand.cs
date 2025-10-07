using MediatR;
using MotorcycleFlow.Application.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleFlow.Application.Features.DeleveryPerson.Commands
{
    public record UploadLicenseImageCommand(
    string DeliveryPersonId,
    string DriverLicenseImageBase64
  ) : IRequest<Result<string>>;
}
