using MotorcycleFlow.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleFlow.Application.Interfaces
{
    public interface IDeliveryPersonRepository
    {
        Task<DeliveryPerson> GetByIdAsync(string id);
        Task<DeliveryPerson> GetByCnpjAsync(string cnpj);
        Task<DeliveryPerson> GetByDriverLicenseNumberAsync(string driverLicenseNumber);
        Task AddAsync(DeliveryPerson deliveryPerson);
        Task UpdateAsync(DeliveryPerson deliveryPerson);
    }
}
