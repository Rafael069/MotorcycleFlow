using MediatR;
using MotorcycleFlow.Application.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleFlow.Application.Features.Motorcycles.Commands
{
   public record DeleteMotorcycleCommand(string MotorcycleId) : IRequest<Result<bool>>;
}
