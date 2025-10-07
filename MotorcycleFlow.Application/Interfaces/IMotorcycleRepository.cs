using MotorcycleFlow.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleFlow.Application.Interfaces
{
    public interface IMotorcycleRepository
    {
        Task<Motorcycle> GetByIdAsync(string id);
        Task<List<Motorcycle>> GetAllAsync(string? licensePlate = null);
        Task<Motorcycle> GetByLicensePlateAsync(string licensePlate);
        Task AddAsync(Motorcycle motorcycle);
        Task UpdateAsync(Motorcycle motorcycle);
        Task DeleteAsync(Motorcycle motorcycle);
    }
}
