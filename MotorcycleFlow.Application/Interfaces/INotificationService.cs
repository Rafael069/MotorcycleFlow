using MotorcycleFlow.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleFlow.Application.Interfaces
{
    public interface INotificationService
    {
        Task PublishMotorcycleRegisteredAsync(Motorcycle motorcycle);
        Task PublishRentalCreatedAsync(Rental rental);
    }
}
