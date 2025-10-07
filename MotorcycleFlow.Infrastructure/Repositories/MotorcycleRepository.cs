using Microsoft.EntityFrameworkCore;
using MotorcycleFlow.Application.Interfaces;
using MotorcycleFlow.Core.Entities;
using MotorcycleFlow.Infrastructure.Data;

namespace MotorcycleFlow.Infrastructure.Repositories
{
    public class MotorcycleRepository : IMotorcycleRepository
    {
        private readonly ApplicationDbContext _context;

        public MotorcycleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Motorcycle> GetByIdAsync(string id)
        {
            return await _context.Motorcycles
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<Motorcycle>> GetAllAsync(string? licensePlate = null)
        {
            var query = _context.Motorcycles.AsQueryable();

            if (!string.IsNullOrEmpty(licensePlate))
            {
                query = query.Where(m => m.LicensePlate.Contains(licensePlate));
            }

            return await query.ToListAsync();
        }

        public async Task<Motorcycle> GetByLicensePlateAsync(string licensePlate)
        {
            return await _context.Motorcycles
                .FirstOrDefaultAsync(m => m.LicensePlate == licensePlate);
        }

        public async Task AddAsync(Motorcycle motorcycle)
        {
            await _context.Motorcycles.AddAsync(motorcycle);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Motorcycle motorcycle)
        {
            _context.Motorcycles.Update(motorcycle);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Motorcycle motorcycle)
        {
            _context.Motorcycles.Remove(motorcycle);
            await _context.SaveChangesAsync();
        }
    }
}