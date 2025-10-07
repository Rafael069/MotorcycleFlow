using MotorcycleFlow.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorcycleFlow.Application.Interfaces
{
    public interface IRentalRepository
    {
        Task<Rental> GetByIdAsync(string id);
        Task<List<Rental>> GetActiveRentalsAsync(string motorcycleId);
        Task<bool> HasActiveRentalsAsync(string motorcycleId);
        Task AddAsync(Rental rental);
        Task UpdateAsync(Rental rental);
    }
}
